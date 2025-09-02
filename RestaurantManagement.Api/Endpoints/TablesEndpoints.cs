using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class TablesEndpoints
{
    public static RouteGroupBuilder MapTablesEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] ITablesService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/restaurant/{restaurantId:guid}", async (Guid restaurantId, [FromServices] ITablesService service) =>
        {
            var serviceResponse = await service.GetByRestaurantIdAsync(restaurantId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:int}", async (int id, [FromServices] ITablesService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateTablesDto dto, [FromServices] ITablesService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:int}", async (int id, UpdateTablesDto dto, [FromServices] ITablesService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:int}", async (int id, [FromServices] ITablesService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}