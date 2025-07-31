using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Api.Endpoints;
public static class OrderDetailEndpoints
{
    public static RouteGroupBuilder MapOrderDetailEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid orderId, [FromServices] IOrderDetailService service) =>
        {
            var data = await service.GetAllAsync(orderId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IOrderDetailService service) =>
        {
            var detail = await service.GetByIdAsync(id);
            return detail != null
                ? Results.Ok(ApiResponse.Success(detail))
                : Results.NotFound(ApiResponse.Fail("OrderDetail not found"));
        });

        group.MapPost("/", async (List<CreateOrderDetailDto> dtos, [FromServices] IOrderDetailService service) =>
        {
            var created = await service.CreateAsync(dtos);
            return Results.Created($"api/orderdetails/", ApiResponse.Success(created));
        });



        group.MapPut("/{id:guid}", async (Guid id, UpdateOrderDetailDto dto, [FromServices] IOrderDetailService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("OrderDetail not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IOrderDetailService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("OrderDetail not found"));
        });

        return group;
    }
}