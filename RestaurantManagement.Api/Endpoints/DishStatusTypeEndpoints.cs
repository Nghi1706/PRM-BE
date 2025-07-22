using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class DishStatusTypeEndpoints
{
    public static RouteGroupBuilder MapDishStatusTypeEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IDishStatusTypeService service) =>
        {
            var data = await service.GetAllAsync();
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:int}", async (int id, [FromServices] IDishStatusTypeService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item != null
                ? Results.Ok(ApiResponse.Success(item))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy trạng thái bàn"));
        });

        group.MapPost("/", async (CreateDishStatusTypeDto dto, [FromServices] IDishStatusTypeService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/Dishstatustypes/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:int}", async (int id, UpdateDishStatusTypeDto dto, [FromServices] IDishStatusTypeService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("DishStatusType not found"));
        });

        group.MapDelete("/{id:int}", async (int id, [FromServices] IDishStatusTypeService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("DishStatusType not found"));
        });

        return group;
    }
}