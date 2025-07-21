using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Api.Endpoints;
public static class TableSessionEndpoints
{
    public static RouteGroupBuilder MapTableSessionEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromQuery] Guid tableId, [FromServices] ITableSessionService service) =>
        {
            var data = await service.GetAllAsync(tableId);
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] ITableSessionService service) =>
        {
            var session = await service.GetByIdAsync(id);
            return session != null
                ? Results.Ok(ApiResponse.Success(session))
                : Results.NotFound(ApiResponse.Fail("TableSession not found"));
        });

        group.MapPost("/", async (CreateTableSessionDto dto, [FromServices] ITableSessionService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/tablesessions/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateTableSessionDto dto, [FromServices] ITableSessionService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("TableSession not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] ITableSessionService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("TableSession not found"));
        });

        return group;
    }
}