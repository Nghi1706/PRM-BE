using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;
using System;

namespace Common.Utilities
{
    public class RabbitMqConfigHelper : IAsyncDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMqConfigHelper> _logger;
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMqConfigHelper(IConfiguration configuration, ILogger<RabbitMqConfigHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            _factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
                UserName = _configuration["RabbitMQ:Username"] ?? "guest",
                Password = _configuration["RabbitMQ:Password"] ?? "guest",
                VirtualHost = _configuration["RabbitMQ:VirtualHost"] ?? "/",
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672"),
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };
        }

        private async Task<IConnection> GetConnectionAsync()
        {
            if (_connection?.IsOpen != true)
            {
                try
                {
                    _connection = await _factory.CreateConnectionAsync();
                    _logger.LogInformation("RabbitMQ connection established");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create RabbitMQ connection");
                    throw;
                }
            }
            return _connection;
        }

        private async Task<IChannel> GetChannelAsync()
        {
            // Ensure connection is established
            if (_connection?.IsOpen != true)
            {
                await GetConnectionAsync();
            }

            // (Re)create channel if missing or closed
            if (_channel?.IsOpen != true)
            {
                try
                {
                    var connection = await GetConnectionAsync();
                    _channel = await connection.CreateChannelAsync();
                    _logger.LogInformation("RabbitMQ channel created");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create RabbitMQ channel");
                    throw;
                }
            }

            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ channel is null after creation attempt.");
            }
            return _channel;
        }

        public async Task PublishAsync(string exchangeName, string routingKey, string message)
        {
            try
            {
                var channel = await GetChannelAsync();
                await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, durable: true);

                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync<BasicProperties>(
                    exchange: exchangeName,
                    routingKey: routingKey,
                    mandatory: false,
                    basicProperties: new BasicProperties
                    {
                        ContentType = "text/plain",
                        DeliveryMode = DeliveryModes.Persistent
                    },
                    body: body
                );


                _logger.LogInformation("Message published to {Exchange} with routing key {RoutingKey}: {Message}", 
                    exchangeName, routingKey, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish message to {Exchange}", exchangeName);
                throw;
            }
        }

        public async Task ConsumeAsync(string exchangeName, string routingKey, string queueName, Func<string, Task> onMessage)
        {
            try
            {
                var channel = await GetChannelAsync();
                
                await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, durable: true);
                await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
                await channel.QueueBindAsync(queueName, exchangeName, routingKey);

                // Set QoS
                await channel.BasicQosAsync(0, 1, false);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (sender, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var msg = Encoding.UTF8.GetString(body);
                        var messageId = ea.BasicProperties.MessageId ?? "unknown";

                        _logger.LogInformation("Received message {MessageId} from {Exchange}: {Message}", 
                            messageId, exchangeName, msg);

                        await onMessage(msg);
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                        
                        _logger.LogInformation("Message {MessageId} processed successfully", messageId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to process message from {Exchange}", exchangeName);
                        await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                    }
                };

                await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
                _logger.LogInformation("Started consuming from queue {Queue} on exchange {Exchange}", queueName, exchangeName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start consuming from {Exchange}", exchangeName);
                throw;
            }
        }

        public async Task ConsumeAsync(string exchangeName, string routingKey, string queueName, int maxRetries, Func<string, Task> onMessage)
        {
            try
            {
                var channel = await GetChannelAsync();
                await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, durable: true);
                await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
                await channel.QueueBindAsync(queueName, exchangeName, routingKey);

                await channel.BasicQosAsync(0, 1, false);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var msg = Encoding.UTF8.GetString(body);
                    var messageId = ea.BasicProperties.MessageId ?? "unknown";
                    try
                    {
                        _logger.LogInformation("Received message {MessageId} from {Exchange}: {Message}", messageId, exchangeName, msg);
                        await onMessage(msg);
                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                        _logger.LogInformation("Message {MessageId} processed successfully", messageId);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            var headers = ea.BasicProperties.Headers ?? new Dictionary<string, object?>();
                            var currentRetry = 0;
                            if (headers.TryGetValue("x-retry-count", out var value))
                            {
                                if (value is byte[] bytes)
                                {
                                    var str = Encoding.UTF8.GetString(bytes);
                                    int.TryParse(str, out currentRetry);
                                }
                                else if (value is int i)
                                {
                                    currentRetry = i;
                                }
                            }

                            if (currentRetry < maxRetries)
                            {
                                var nextRetry = currentRetry + 1;
                                headers["x-retry-count"] = Encoding.UTF8.GetBytes(nextRetry.ToString());

                                var props = new BasicProperties
                                {
                                    ContentType = ea.BasicProperties.ContentType ?? "application/json",
                                    DeliveryMode = DeliveryModes.Persistent,
                                    MessageId = messageId,
                                    CorrelationId = ea.BasicProperties.CorrelationId,
                                    Headers = headers
                                };

                                await channel.BasicPublishAsync<BasicProperties>(
                                    exchange: exchangeName,
                                    routingKey: routingKey,
                                    mandatory: false,
                                    basicProperties: props,
                                    body: body);

                                await channel.BasicAckAsync(ea.DeliveryTag, false);
                                _logger.LogWarning(ex, "Retry {Retry}/{MaxRetries} for message {MessageId}", nextRetry, maxRetries, messageId);
                            }
                            else
                            {
                                await channel.BasicAckAsync(ea.DeliveryTag, false);
                                _logger.LogError(ex, "Exceeded max retries ({MaxRetries}) for message {MessageId}. Discarding.", maxRetries, messageId);
                            }
                        }
                        catch (Exception republishEx)
                        {
                            _logger.LogError(republishEx, "Failed during retry handling for message {MessageId}", messageId);
                            await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                        }
                    }
                };

                await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
                _logger.LogInformation("Started consuming (with retries) from queue {Queue} on exchange {Exchange}", queueName, exchangeName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start consuming (with retries) from {Exchange}", exchangeName);
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_channel?.IsOpen == true)
                {
                    await _channel.CloseAsync();
                    _logger.LogInformation("RabbitMQ channel closed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while closing RabbitMQ channel");
            }
            finally
            {
                _channel = null;
            }

            try
            {
                if (_connection?.IsOpen == true)
                {
                    await _connection.CloseAsync();
                    _logger.LogInformation("RabbitMQ connection closed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while closing RabbitMQ connection");
            }
            finally
            {
                _connection = null;
            }
        }
    }
}
