using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/restaurant/{restaurantId:guid}", async (Guid restaurantId, [FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.GetByRestaurantIdAsync(restaurantId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/email/{email}", async (string email, [FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.GetByEmailAsync(email);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateUsersDto dto, [FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateUsersDto dto, [FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IUsersService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}
