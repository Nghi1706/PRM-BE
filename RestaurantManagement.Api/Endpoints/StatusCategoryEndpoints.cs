using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class StatusCategoryEndpoints
{
    public static RouteGroupBuilder MapStatusCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IStatusCategoryService service) =>
        {
            var data = await service.GetAllAsync();
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IStatusCategoryService service) =>
        {
            var data = await service.GetByIdAsync(id);
            return data != null
                ? Results.Ok(ApiResponse.Success(data))
                : Results.NotFound(ApiResponse.Fail("StatusCategory not found"));
        });

        group.MapPost("/", async (CreateStatusCategoryDto dto, [FromServices] IStatusCategoryService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/v1/statuscategories/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateStatusCategoryDto dto, [FromServices] IStatusCategoryService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("StatusCategory not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IStatusCategoryService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("StatusCategory not found"));
        });

        return group;
    }
}
