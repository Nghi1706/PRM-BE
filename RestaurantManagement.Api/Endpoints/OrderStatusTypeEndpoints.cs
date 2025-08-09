using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class OrderStatusTypeEndpoints
{
    public static RouteGroupBuilder MapOrderStatusTypeEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IOrderStatusTypeService service) =>
        {
            var data = await service.GetAllAsync();
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/{id:int}", async (int id, [FromServices] IOrderStatusTypeService service) =>
        {
            var item = await service.GetByIdAsync(id);
            if (item != null)
            {
                var response = ApiResponse.Success(item);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Không tìm thấy trạng thái order");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapPost("/", async (CreateOrderStatusTypeDto dto, [FromServices] IOrderStatusTypeService service) =>
        {
            var created = await service.CreateAsync(dto);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:int}", async (int id, UpdateOrderStatusTypeDto dto, [FromServices] IOrderStatusTypeService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("OrderStatusType not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:int}", async (int id, [FromServices] IOrderStatusTypeService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("OrderStatusType not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}