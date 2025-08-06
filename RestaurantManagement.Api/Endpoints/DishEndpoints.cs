using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class DishEndpoints
{
    public static RouteGroupBuilder MapDishEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromQuery] Guid? categoryId, [FromServices] IDishService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IDishService service) =>
        {
            var dish = await service.GetByIdAsync(id);
            if (dish != null)
            {
                var response = ApiResponse.Success(dish);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Không tìm thấy món ăn");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapGet("/category/{categoryId:guid}", async (Guid categoryId, [FromServices] IDishService service) =>
        {
            var dishes = await service.GetByIdCategoryAsync(categoryId);
            if (dishes != null)
            {
                var response = ApiResponse.Success(dishes);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Không tìm thấy món ăn");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapPost("/", async (CreateDishDto dto, [FromServices] IDishService service) =>
        {
            var created = await service.CreateAsync(dto);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateDishDto dto, [FromServices] IDishService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Dish not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IDishService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Dish not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}