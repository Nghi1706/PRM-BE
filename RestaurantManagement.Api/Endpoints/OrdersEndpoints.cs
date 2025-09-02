using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class OrdersEndpoints
{
    public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IOrdersService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/table/{tableId:int}", async (int tableId, [FromServices] IOrdersService service) =>
        {
            var serviceResponse = await service.GetByTableIdAsync(tableId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IOrdersService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateOrdersDto dto, [FromServices] IOrdersService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateOrdersDto dto, [FromServices] IOrdersService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IOrdersService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}