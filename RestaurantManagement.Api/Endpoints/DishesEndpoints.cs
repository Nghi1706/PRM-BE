using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class DishEndpoints
{
    public static RouteGroupBuilder MapDishEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/restaurant/{restaurantId:guid}", async (Guid restaurantId, [FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.GetByRestaurantIdAsync(restaurantId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/category/{categoryId:int}", async (int categoryId, [FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.GetByCategoryIdAsync(categoryId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:int}", async (int id, [FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateDishesDto dto, [FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:int}", async (int id, UpdateDishesDto dto, [FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:int}", async (int id, [FromServices] IDishesService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}