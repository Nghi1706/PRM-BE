using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromServices] ICategoryService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] ICategoryService service) =>
        {
            var category = await service.GetByIdAsync(id);
            if (category != null)
            {
                var response = ApiResponse.Success(category);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Không tìm thấy danh mục");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapPost("/", async (CreateCategoryDto dto, [FromServices] ICategoryService service) =>
        {
            var created = await service.CreateAsync(dto);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateCategoryDto dto, [FromServices] ICategoryService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Category not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] ICategoryService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Category not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}