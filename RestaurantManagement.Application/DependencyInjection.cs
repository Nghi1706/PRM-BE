using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Services;

namespace RestaurantManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStatusCategoryService, StatusCategoryService>();
        // services.AddScoped<IStatusService, StatusService>();
        // services.AddScoped<IRestaurantService, RestaurantService>();
        //services.AddScoped<IRoleServices, RoleService>();
        // services.AddScoped<IUserService, UserService>();
        services.AddScoped<AuthService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ITableService, TableService>();
        // services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderDetailService, OrderDetailService>();
        services.AddScoped<ITableSessionService, TableSessionService>();
        services.AddScoped<IOrderStatusTypeService, OrderStatusTypeService>();
        services.AddScoped<IDishStatusTypeService, DishStatusTypeService>();
        services.AddScoped<ITableStatusTypeService, TableStatusTypeService>();

        //v2

        services.AddScoped<IRolesServices, RolesService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IVouchersService, VouchersService>();
        services.AddScoped<IRestaurantsService, RestaurantsService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        return services;
    }
}
