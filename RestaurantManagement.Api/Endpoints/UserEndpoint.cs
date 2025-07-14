using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Api.Endpoints
{
    public static class UserEndpoint
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/{RestaurantId:guid}", async (Guid RestaurantId, [FromServices] IUserService service) =>
            {
                var data = await service.GetAllAsync(RestaurantId);
                return Results.Ok(ApiResponse.Success(data));
            });
            group.MapPost("/", async (CreateUserDto dto, [FromServices] IUserService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"api/users/{created.Id}", ApiResponse.Success(created));
            });
            group.MapPut("/{id:guid}", async (Guid id, UpdateUserDto dto, [FromServices] IUserService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated
                    ? Results.Ok(ApiResponse.Success("Updated successfully"))
                    : Results.NotFound(ApiResponse.Fail("User not found"));
            });
            group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IUserService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                return deleted
                    ? Results.Ok(ApiResponse.Success("Deleted successfully"))
                    : Results.NotFound(ApiResponse.Fail("User not found"));
            });
            return group;
        }
    }
}
