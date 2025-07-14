using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
namespace RestaurantManagement.Api.Endpoints
{
    public static class RoleEndpoint
    {
        public static RouteGroupBuilder MapRoleEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IRoleServices service) =>
            {
                var roles = await service.GetAllAsync();
                return Results.Ok(ApiResponse.Success(roles));
            });
            group.MapGet("/{id:guid}", async (Guid id, [FromServices] IRoleServices service) =>
            {
                var role = await service.GetByIdAsync(id);
                return role != null
                    ? Results.Ok(ApiResponse.Success(role))
                    : Results.NotFound(ApiResponse.Fail("Role not found"));
            });
            group.MapPost("/", async (CreateRoleDto dto, [FromServices] IRoleServices service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"api/roles/{created.Id}", ApiResponse.Success(created));
            });
            group.MapPut("/{id:guid}", async (Guid id, UpdateRoleDto dto, [FromServices] IRoleServices service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated
                    ? Results.Ok(ApiResponse.Success("Updated successfully"))
                    : Results.NotFound(ApiResponse.Fail("Role not found"));
            });
            group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IRoleServices service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted
                    ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                    : Results.NotFound(ApiResponse.Fail("Role not found"));
            });
            return group;
        }
    }
}
