using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Api.Endpoints;
public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromServices] IOrderService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{roomId:guid}", async (Guid roomId, [FromServices] IOrderService service) =>
        {
            var order = await service.GetByIdAsync(roomId);
            return order != null
                ? Results.Ok(ApiResponse.Success(order))
                : Results.NotFound(ApiResponse.Fail("Order not found"));
        });

        group.MapPost("/", async (CreateOrderDto dto, [FromServices] IOrderService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/orders/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateOrderDto dto, [FromServices] IOrderService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Order not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IOrderService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Order not found"));
        });

        return group;
    }
}