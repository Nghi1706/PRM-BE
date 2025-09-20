using Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddCommonConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            services.Configure<MessageQueueSettings>(configuration.GetSection("MessageQueue"));
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));
            
            return services;
        }
    }
}
