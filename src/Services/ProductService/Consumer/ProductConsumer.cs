using Common.Configurations;
using Common.Domain.Interfaces;
using Microsoft.Extensions.Options;
using ProductService.Services;

namespace ProductService.Consumer
{
    public class ProductConsumer : BackgroundService
    {
        private readonly ILogger<ProductConsumer> _logger;
        private readonly IMessageQueueService _messageQueueService;
        private readonly IOptions<RabbitMqSettings> _settings;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProductConsumer(
            ILogger<ProductConsumer> logger,
            IMessageQueueService messageQueueService,
            IOptions<RabbitMqSettings> settings,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _messageQueueService = messageQueueService;
            _settings = settings;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("ProductConsumer starting...");

                // Check connection first
                var isConnected = await _messageQueueService.IsConnectedAsync();
                if (!isConnected)
                {
                    _logger.LogError("RabbitMQ connection failed");
                    return;
                }

                _logger.LogInformation("RabbitMQ connection established successfully");

                // Subscribe to order viewed messages
                _logger.LogInformation("Subscribing to product-order-queue for OrderViewedMessage");
                await _messageQueueService.SubscribeAsync<OrderViewedMessage>(
                    exchangeName: "order-exchange",
                    routingKey: "order.viewed",
                    queueName: "product-order-queue",
                    handler: async (message) =>
                    {
                        try
                        {
                            _logger.LogInformation("Received OrderViewedMessage: OrderId={OrderId}, OrderNumber={OrderNumber}", 
                                message.OrderId, message.OrderNumber);
                            
                            using var scope = _scopeFactory.CreateScope();
                            var svc = scope.ServiceProvider.GetRequiredService<IProductService>();
                            await svc.ProcessOrderViewedMessageAsync(message, stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error processing OrderViewedMessage");
                        }
                    });

                _logger.LogInformation("ProductConsumer subscriptions completed");

                // Keep the service running
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProductConsumer failed to start");
                throw;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ProductConsumerService");
            await _messageQueueService.DisposeAsync();
            await base.StopAsync(cancellationToken);
        }
    }

    public class OrderViewedMessage
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public List<int> ProductIds { get; set; } = new();
        public DateTime ViewedAt { get; set; }
    }
}
