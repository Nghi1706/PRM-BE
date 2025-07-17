using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class DishEndpoints
{
    public static RouteGroupBuilder MapDishEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid restaurantId, [FromQuery] Guid? categoryId, [FromServices] IDishService service) =>
        {
            var data = await service.GetAllAsync(restaurantId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IDishService service) =>
        {
            var dish = await service.GetByIdAsync(id);
            return dish != null
                ? Results.Ok(ApiResponse.Success(dish))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy món ăn"));
        });
        group.MapGet("/category/{categoryId:guid}", async (Guid categoryId, [FromServices] IDishService service) =>
        {
            var dishes = await service.GetByIdCategoryAsync(categoryId);

            return dishes != null 
                ? Results.Ok(ApiResponse.Success(dishes)) 
                : Results.NotFound(ApiResponse.Fail("không tìm thấy món ăn"));
        });

        group.MapPost("/", async (CreateDishDto dto, [FromServices] IDishService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/dishes/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateDishDto dto, [FromServices] IDishService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Dish not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IDishService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Dish not found"));
        });

        return group;
    }
}