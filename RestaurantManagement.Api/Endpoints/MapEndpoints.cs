using Microsoft.AspNetCore.Routing;

namespace RestaurantManagement.Api.Endpoints;

public static class MapEndpoints
{
    // 👇 Thay vì "this WebApplication app", dùng "this RouteGroupBuilder group"
    public static RouteGroupBuilder MapAppEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/health", () => Results.Ok("Healthy"));
        // status categories endpoint
        group.MapGroup("/statuscategories").MapStatusCategoryEndpoints();
        // status endpoint
        group.MapGroup("/statuses").MapStatusEndpoints();

        // Các group khác:
        // group.MapGroup("/orders").MapOrderEndpoints();

        return group;
    }
}
