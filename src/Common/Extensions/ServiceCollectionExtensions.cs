using Common.Application.Interfaces;
using Common.Application.Services;
using Common.Configurations;
using Common.Domain.Interfaces;
using Common.Infrastructure.Database;
using Common.Infrastructure.MessageQueue.RabbitMQ;
using Common.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            
            // Message Queue
            services.Configure<MessageQueueSettings>(configuration.GetSection("MessageQueue"));
            services.AddSingleton<IMessageQueueService, RabbitMQService>();
            
            // Application Services
            services.AddScoped<IMessageQueueApplication, MessageQueueApplication>();
            services.AddScoped<IDatabaseApplication, DatabaseApplication>();
            
            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            
            return services;
        }

        public static IServiceCollection AddDatabaseContext<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName = "DefaultConnection") where T : BaseDbContext
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);
            return services.AddDatabaseContext<T>(connectionString: connectionString);
        }
    }
}
