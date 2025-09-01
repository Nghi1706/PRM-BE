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

            // v1
            services.AddScoped<IStatusCategoryRepository, StatusCategoryRepository>();

            // services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();

            // services.AddScoped<IRoleRepository, RolesRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<ITableRepository, TableRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IDishRepository, DishRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            services.AddScoped<ITableSessionRepository, TableSessionRepository>();

            services.AddScoped<ITableStatusTypeRepository, TableStatusTypeRepository>();

            services.AddScoped<IOrderStatusTypeRepository, OrderStatusTypeRepository>();

            services.AddScoped<IDishStatusTypeRepository, DishStatusTypeRepository>();

            // v2 

            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();



            return services;
        }
    }
}
