using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class RestaurantEndpoints
{
    public static RouteGroupBuilder MapRestaurantEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IRestaurantsService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IRestaurantsService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateRestaurantsDto dto, [FromServices] IRestaurantsService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateRestaurantsDto dto, [FromServices] IRestaurantsService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IRestaurantsService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}