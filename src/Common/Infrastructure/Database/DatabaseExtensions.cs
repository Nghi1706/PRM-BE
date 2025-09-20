using Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Database
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseContext<T>(
            this IServiceCollection services,
            string connectionString) where T : BaseDbContext
        {
            services.AddDbContext<T>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IDatabaseContext, T>();
            services.AddScoped<IDatabaseContextFactory<T>, DatabaseContextFactory<T>>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext<T>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction) where T : BaseDbContext
        {
            services.AddDbContext<T>(optionsAction);
            services.AddScoped<IDatabaseContext, T>();
            services.AddScoped<IDatabaseContextFactory<T>, DatabaseContextFactory<T>>();

            return services;
        }
    }
}
