using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class OrderStatusTypeEndpoints
{
    public static RouteGroupBuilder MapOrderStatusTypeEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IOrderStatusTypeService service) =>
        {
            var data = await service.GetAllAsync();
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:int}", async (int id, [FromServices] IOrderStatusTypeService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item != null
                ? Results.Ok(ApiResponse.Success(item))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy trạng thái order"));
        });

        group.MapPost("/", async (CreateOrderStatusTypeDto dto, [FromServices] IOrderStatusTypeService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/Orderstatustypes/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:int}", async (int id, UpdateOrderStatusTypeDto dto, [FromServices] IOrderStatusTypeService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("OrderStatusType not found"));
        });

        group.MapDelete("/{id:int}", async (int id, [FromServices] IOrderStatusTypeService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("OrderStatusType not found"));
        });

        return group;
    }
}