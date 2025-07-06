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
        return services;
    }
}
