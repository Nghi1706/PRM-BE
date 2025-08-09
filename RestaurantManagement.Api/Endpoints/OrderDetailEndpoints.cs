using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class OrderDetailEndpoints
{
    public static RouteGroupBuilder MapOrderDetailEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid orderId, [FromServices] IOrderDetailService service) =>
        {
            var data = await service.GetAllAsync(orderId);
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IOrderDetailService service) =>
        {
            var detail = await service.GetByIdAsync(id);
            if (detail != null)
            {
                var response = ApiResponse.Success(detail);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("OrderDetail not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapPost("/", async (List<CreateOrderDetailDto> dtos, [FromServices] IOrderDetailService service) =>
        {
            var created = await service.CreateAsync(dtos);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateOrderDetailDto dto, [FromServices] IOrderDetailService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("OrderDetail not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IOrderDetailService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("OrderDetail not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}