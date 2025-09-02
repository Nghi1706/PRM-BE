using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class OrderDetailsEndpoints
{
    public static RouteGroupBuilder MapOrderDetailsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IOrderDetailsService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/order/{orderId:guid}", async (Guid orderId, [FromServices] IOrderDetailsService service) =>
        {
            var serviceResponse = await service.GetByOrderIdAsync(orderId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IOrderDetailsService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateOrderDetailsDto dto, [FromServices] IOrderDetailsService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateOrderDetailsDto dto, [FromServices] IOrderDetailsService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IOrderDetailsService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}