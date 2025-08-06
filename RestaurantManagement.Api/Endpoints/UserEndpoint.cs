using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Shared.Response;

namespace RestaurantManagement.Api.Endpoints
{
    public static class UserEndpoint
    {
        public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/{RestaurantId:guid}", async (Guid RestaurantId, [FromServices] IUserService service) =>
            {
                var data = await service.GetAllAsync(RestaurantId);
                var response = ApiResponse.Success(data);
                return Results.Json(response, statusCode: response.StatusCode);
            });

            group.MapPost("/", async (CreateUserDto dto, [FromServices] IUserService service) =>
            {
                var created = await service.CreateAsync(dto);
                var response = ApiResponse.Created(created);
                return Results.Json(response, statusCode: response.StatusCode);
            });

            group.MapPut("/{id:guid}", async (Guid id, UpdateUserDto dto, [FromServices] IUserService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                if (updated)
                {
                    var response = ApiResponse.Success(null, "Updated successfully");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
                else
                {
                    var response = ApiResponse.NotFound("User not found");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
            });

            group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IUserService service) =>
            {
                var deleted = await service.DeleteAsync(id);
                if (deleted)
                {
                    var response = ApiResponse.Success(null, "Deleted successfully");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
                else
                {
                    var response = ApiResponse.NotFound("User not found");
                    return Results.Json(response, statusCode: response.StatusCode);
                }
            });
            return group;
        }
    }
}
