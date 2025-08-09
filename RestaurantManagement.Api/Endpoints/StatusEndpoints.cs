using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class StatusEndpoints
{
    public static RouteGroupBuilder MapStatusEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IStatusService service) =>
        {
            var data = await service.GetAllAsync();
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPost("/", async (CreateStatusDto dto, [FromServices] IStatusService service) =>
        {
            var created = await service.CreateAsync(dto);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateStatusDto dto, [FromServices] IStatusService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Status not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IStatusService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("Status not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}
