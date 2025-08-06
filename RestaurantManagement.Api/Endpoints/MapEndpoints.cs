using Microsoft.AspNetCore.Routing;

namespace RestaurantManagement.Api.Endpoints;

public static class MapEndpoints
{
    // 👇 Thay vì "this WebApplication app", dùng "this RouteGroupBuilder group"
    public static RouteGroupBuilder MapAppEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/health", () => Results.Ok("Healthy"));
        // status categories endpoint
        group.MapGroup("/statuscategories").RequireAuthorization().MapStatusCategoryEndpoints();
        // status endpoint
        group.MapGroup("/statuses").RequireAuthorization().MapStatusEndpoints();
        // restaurant endpoint
        group.MapGroup("/restaurants").RequireAuthorization().MapRestaurantEndpoints();
        // role endpoint
        group.MapGroup("/roles").RequireAuthorization().MapRoleEndpoints();
        // user endpoint
        group.MapGroup("/users").RequireAuthorization().MapUserEndpoints();
        // auth endpoint
        group.MapGroup("/auth").MapAuthEndpoints();
        // room endpoint
        group.MapGroup("/rooms").MapRoomEndpoints();
        // table endpoint
        group.MapGroup("/tables").RequireAuthorization().MapTableEndpoints();
        // category endpoint
        group.MapGroup("/categories").RequireAuthorization().MapCategoryEndpoints();
        // dish endpoint
        group.MapGroup("/dishes").RequireAuthorization().MapDishEndpoints();
        // order endpoint
        group.MapGroup("/orders").MapOrderEndpoints();
        // order detail endpoint
        group.MapGroup("/orderdetails").MapOrderDetailEndpoints();
        // table session endpoint
        group.MapGroup("/tablesessions").MapTableSessionEndpoints();
        // table status type endpoint
        group.MapGroup("/tablestatustypes").RequireAuthorization().MapTableStatusTypeEndpoints();
        // order status type endpoint
        group.MapGroup("/orderstatustypes").RequireAuthorization().MapOrderStatusTypeEndpoints();
        // dish status type endpoint
        group.MapGroup("/dishstatustypes").RequireAuthorization().MapDishStatusTypeEndpoints();


        return group;
    }
}
