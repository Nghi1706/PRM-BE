using Microsoft.AspNetCore.Routing;

namespace RestaurantManagement.Api.Endpoints;

public static class MapEndpoints
{
    // 👇 Thay vì "this WebApplication app", dùng "this RouteGroupBuilder group"
    public static RouteGroupBuilder MapAppEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/health", () => Results.Ok("Healthy"));
        group.MapGroup("/statuscategories").MapStatusCategoryEndpoints();

        // Các group khác:
        // group.MapGroup("/orders").MapOrderEndpoints();

        return group;
    }
}
