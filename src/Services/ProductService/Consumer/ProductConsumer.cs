using Common.Utilities;
using Common.Configurations;
using Microsoft.Extensions.Options;
using ProductService.Services;

namespace ProductService.Consumer
{
    public class ProductConsumer : BackgroundService
    {
        private readonly ILogger<ProductConsumer> _logger;
        private readonly RabbitMqConfigHelper _rabbitMqHelper;
        private readonly IOptions<RabbitMqSettings> _settings;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProductConsumer(
            ILogger<ProductConsumer> logger,
            RabbitMqConfigHelper rabbitMqHelper,
            IOptions<RabbitMqSettings> settings,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _rabbitMqHelper = rabbitMqHelper;
            _settings = settings;
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _rabbitMqHelper.ConsumeAsync(
                exchangeName: _settings.Value.Exchange,
                routingKey: _settings.Value.RoutingKey,
                queueName: _settings.Value.Queue,
                maxRetries: _settings.Value.MaxRetries,
                onMessage: async msg =>
                {
                    using var scope = _scopeFactory.CreateScope();
                    var svc = scope.ServiceProvider.GetRequiredService<IProductService>();
                    await svc.ProcessOrderMessageAsync(msg, stoppingToken);
                });
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ProductConsumerService");
            await _rabbitMqHelper.DisposeAsync();
            await base.StopAsync(cancellationToken);
        }
    }

    
}
