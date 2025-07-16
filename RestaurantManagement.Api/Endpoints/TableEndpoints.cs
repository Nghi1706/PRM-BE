using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Api.Endpoints;
public static class TableEndpoints
{
    public static RouteGroupBuilder MapTableEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid roomId, [FromServices] ITableService service) =>
        {
            var data = await service.GetAllAsync(roomId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] ITableService service) =>
        {
            var table = await service.GetByIdAsync(id);
            return table != null
                ? Results.Ok(ApiResponse.Success(table))
                : Results.NotFound(ApiResponse.Fail("Không tìm thấy bàn"));
        });

        group.MapPost("/", async (CreateTableDto dto, [FromServices] ITableService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/tables/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateTableDto dto, [FromServices] ITableService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Table not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] ITableService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Table not found"));
        });

        return group;
    }
}