using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints;

public static class StatusCategoryEndpoints
{
    public static RouteGroupBuilder MapStatusCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IStatusCategoryService service) =>
        {
            var data = await service.GetAllAsync();
            var response = ApiResponse.Success(data);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IStatusCategoryService service) =>
        {
            var data = await service.GetByIdAsync(id);
            if (data != null)
            {
                var response = ApiResponse.Success(data);
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("StatusCategory not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapPost("/", async (CreateStatusCategoryDto dto, [FromServices] IStatusCategoryService service) =>
        {
            var created = await service.CreateAsync(dto);
            var response = ApiResponse.Created(created);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateStatusCategoryDto dto, [FromServices] IStatusCategoryService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated)
            {
                var response = ApiResponse.Success(null, "Updated successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("StatusCategory not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IStatusCategoryService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (deleted)
            {
                var response = ApiResponse.Success(null, "Deleted successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var response = ApiResponse.NotFound("StatusCategory not found");
                return Results.Json(response, statusCode: response.StatusCode);
            }
        });

        return group;
    }
}
