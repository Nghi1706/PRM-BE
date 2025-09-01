using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class VoucherEndpoints
{
    public static RouteGroupBuilder MapVoucherEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.GetAllAsync();
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/restaurant/{restaurantId:guid}", async (Guid restaurantId, [FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.GetByRestaurantIdAsync(restaurantId);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/{id:guid}", async (Guid id, [FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.GetByIdAsync(id);
            return serviceResponse.ToApiResult();
        });

        group.MapGet("/code/{code}", async (string code, [FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.GetByCodeAsync(code);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/", async (CreateVoucherDto dto, [FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.CreateAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateVoucherDto dto, [FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.UpdateAsync(id, dto);
            return serviceResponse.ToApiResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, [FromServices] IVouchersService service) =>
        {
            var serviceResponse = await service.DeleteAsync(id);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}