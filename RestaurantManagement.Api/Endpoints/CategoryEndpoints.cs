using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromServices] ICategoryService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] ICategoryService service) =>
        {
            var category = await service.GetByIdAsync(id);
            return category != null
                ? Results.Ok(ApiResponse.Success(category))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy danh mục"));
        });

        group.MapPost("/", async (CreateCategoryDto dto, [FromServices] ICategoryService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/categories/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateCategoryDto dto, [FromServices] ICategoryService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Category not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] ICategoryService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Category not found"));
        });

        return group;
    }
}