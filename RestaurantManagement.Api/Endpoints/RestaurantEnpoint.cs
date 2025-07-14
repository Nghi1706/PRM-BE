using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class RestaurantEndpoints
{
    public static RouteGroupBuilder MapRestaurantEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IRestaurantService service) =>
        {
            var data = await service.GetAllAsync();
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IRestaurantService service) =>
        {
            var restaurant = await service.GetByIdAsync(id);
            return restaurant != null
                ? Results.Ok(ApiResponse.Success(restaurant))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy nhà hàng"));
        });

        group.MapPost("/", async (CreateRestaurantDto dto, [FromServices] IRestaurantService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/restaurants/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateRestaurantDto dto, [FromServices] IRestaurantService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Restaurant not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IRestaurantService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Restaurant not found"));
        });

        return group;
    }
}