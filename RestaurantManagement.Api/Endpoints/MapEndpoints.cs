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
        group.MapGroup("/status").MapStatusEndpoints();
        // restaurant endpoint
        group.MapGroup("/restaurants").MapRestaurantEndpoints();
        // roles endpoint
        group.MapGroup("/roles").MapRolesEndpoints();
        // vouchers enpoint
        group.MapGroup("/vouchers").MapVoucherEndpoints();
        // user endpoint
        group.MapGroup("/users").MapUserEndpoints();
        // auth endpoint
        group.MapGroup("/auth").MapAuthEndpoints();
        // room endpoint
        group.MapGroup("/rooms").MapRoomEndpoints();
        // table endpoint
        group.MapGroup("/tables").MapTableEndpoints();
        // category endpoint
        group.MapGroup("/categories").MapCategoryEndpoints();
        // dish endpoint
        group.MapGroup("/dishes").MapDishEndpoints();
        // order endpoint
        group.MapGroup("/orders").MapOrderEndpoints();
        // order detail endpoint
        group.MapGroup("/orderdetails").MapOrderDetailEndpoints();
        // table session endpoint
        group.MapGroup("/tablesessions").MapTableSessionEndpoints();
        // table status type endpoint
        group.MapGroup("/tablestatustypes").MapTableStatusTypeEndpoints();
        // order status type endpoint
        group.MapGroup("/orderstatustypes").MapOrderStatusTypeEndpoints();
        // dish status type endpoint
        group.MapGroup("/dishstatustypes").MapDishStatusTypeEndpoints();


        return group;
    }
}
