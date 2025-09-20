using Common.Domain.Interfaces;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Common.Infrastructure.MessageQueue.RabbitMQ
{
    public class RabbitMQService : IMessageQueueService, IAsyncDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMQService> _logger;
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger)
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
            var connection = await GetConnectionAsync();
            if (_channel?.IsOpen != true)
            {
                _channel = await connection.CreateChannelAsync();
                _logger.LogInformation("RabbitMQ channel created");
            }
            return _channel;
        }

        public async Task PublishAsync<T>(string exchange, string routingKey, T message)
        {
            try
            {
                var channel = await GetChannelAsync();
                await channel.ExchangeDeclareAsync(exchange, ExchangeType.Direct, durable: true);
                
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                var properties = new BasicProperties
                {
                    ContentType = "text/plain",
                    DeliveryMode = DeliveryModes.Persistent
                };
                
                await channel.BasicPublishAsync(exchange: exchange, routingKey: routingKey, mandatory: false, basicProperties: properties, body: body);
                
                _logger.LogInformation("Message published to exchange {Exchange} with routing key {RoutingKey}", exchange, routingKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish message to exchange {Exchange} with routing key {RoutingKey}", exchange, routingKey);
                throw;
            }
        }

        public async Task SubscribeAsync<T>(string exchangeName, string routingKey, string queueName, Func<T, Task> handler)
        {
            try
            {
                var channel = await GetChannelAsync();
                await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
                await channel.QueueBindAsync(queueName, exchangeName, routingKey);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var json = Encoding.UTF8.GetString(body);
                        var message = JsonSerializer.Deserialize<T>(json);
                        
                        if (message != null)
                        {
                            await handler(message);
                            await channel.BasicAckAsync(ea.DeliveryTag, false);
                            _logger.LogInformation(message.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing message from queue {QueueName}", queueName);
                        await channel.BasicNackAsync(ea.DeliveryTag, false, true);
                    }
                };
                
                await channel.BasicConsumeAsync(queueName, autoAck: false, consumer);
                _logger.LogInformation("Started consuming messages from queue {QueueName}", queueName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to subscribe to queue {QueueName}", queueName);
                throw;
            }
        }

        public async Task<bool> IsConnectedAsync()
        {
            try
            {
                var connection = await GetConnectionAsync();
                return connection.IsOpen;
            }
            catch
            {
                return false;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel?.IsOpen == true)
            {
                await _channel.CloseAsync();
                await _channel.DisposeAsync();
            }
            
            if (_connection?.IsOpen == true)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
