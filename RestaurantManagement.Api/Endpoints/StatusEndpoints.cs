using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;

public static class StatusEndpoints
{
    public static RouteGroupBuilder MapStatusEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IStatusService service) =>
        {
            var data = await service.GetAllAsync();
            return Results.Ok(ApiResponse.Success(data));
        });

        group.MapPost("/", async (CreateStatusDto dto, [FromServices] IStatusService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"api/status/{created.Id}", ApiResponse.Success(created));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateStatusDto dto, [FromServices] IStatusService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            return updated
                ? Results.Ok(ApiResponse.Success("Updated successfully"))
                : Results.NotFound(ApiResponse.Fail("Status not found"));
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IStatusService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted
                ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                : Results.NotFound(ApiResponse.Fail("Status not found"));
        });

        return group;
    }
}
