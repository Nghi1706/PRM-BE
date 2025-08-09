using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrderEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromServices] IOrderService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IOrderService service) =>
        {
            var order = await service.GetByIdAsync(id);
            if (order != null)
            {
                var response = ApiResponse.Success(order);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Order not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapGet("/room/{roomId:guid}", async (Guid roomId, [FromServices] IOrderService service) =>
        {
            var data = await service.GetByRoomAsync(roomId);
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/table/{tableId:guid}", async (Guid tableId, [FromServices] IOrderService service) =>
        {
            var data = await service.GetByTableAsync(tableId);
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPost("/", async (CreateOrderDto dto, [FromServices] IOrderService service) =>
        {
            var created = await service.CreateAsync(dto);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateOrderDto dto, [FromServices] IOrderService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Order not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IOrderService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Order not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}