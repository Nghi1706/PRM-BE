using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Infrastructure.Data;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Repositories;

namespace RestaurantManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IStatusCategoryRepository, StatusCategoryRepository>();

            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<ITableRepository, TableRepository>();

            return services;
        }
    }
}
