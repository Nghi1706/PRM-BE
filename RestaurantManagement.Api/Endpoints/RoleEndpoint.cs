using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints
{
    public static class RoleEndpoint
    {
        public static RouteGroupBuilder MapRoleEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IRoleServices service) =>
            {
                var roles = await service.GetAllAsync();
                var response = ApiResponse.Success(roles);
                return Results.Json(response, statusCode: response.StatusCode);
            });

            group.MapGet("/{id:guid}", async (Guid id, [FromServices] IRoleServices service) =>
            {
                var role = await service.GetByIdAsync(id);
                if (role != null)
                {
                    var response = ApiResponse.Success(role);
                    return Results.Json(response, statusCode: response.StatusCode);
                }
                else
                {
                    var response = ApiResponse.NotFound("Role not found");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
            });

            group.MapPost("/", async (CreateRoleDto dto, [FromServices] IRoleServices service) =>
            {
                var created = await service.CreateAsync(dto);
                var response = ApiResponse.Created(created);
                return Results.Json(response, statusCode: response.StatusCode);
            });

            group.MapPut("/{id:guid}", async (Guid id, UpdateRoleDto dto, [FromServices] IRoleServices service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                if (updated)
                {
                    var response = ApiResponse.Success(null, "Updated successfully");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
                else
                {
                    var response = ApiResponse.NotFound("Role not found");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
            });

            group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IRoleServices service) =>
            {
                var deleted = await service.DeleteAsync(id);
                if (deleted)
                {
                    var response = ApiResponse.Success(null, "Deleted successfully");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
                else
                {
                    var response = ApiResponse.NotFound("Role not found");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
            });
            return group;
        }
    }
}
