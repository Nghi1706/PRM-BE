using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;

namespace RestaurantManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStatusCategoryService, StatusCategoryService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRoleServices, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<AuthService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDishService, DishService>();
        return services;
    }
}
