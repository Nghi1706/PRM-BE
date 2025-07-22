using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class TableStatusTypeEndpoints
{
    public static RouteGroupBuilder MapTableStatusTypeEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] ITableStatusTypeService service) =>
        {
            var data = await service.GetAllAsync();
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:int}", async (int id, [FromServices] ITableStatusTypeService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item != null
                ? Results.Ok(ApiResponse.Success(item))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy trạng thái bàn"));
        });

        group.MapPost("/", async (CreateTableStatusTypeDto dto, [FromServices] ITableStatusTypeService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/tablestatustypes/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:int}", async (int id, UpdateTableStatusTypeDto dto, [FromServices] ITableStatusTypeService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("TableStatusType not found"));
        });

        group.MapDelete("/{id:int}", async (int id, [FromServices] ITableStatusTypeService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("TableStatusType not found"));
        });

        return group;
    }
}