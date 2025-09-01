using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints
{
    public static class RolesEndpoint
    {
        public static RouteGroupBuilder MapRolesEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([FromServices] IRolesServices service) =>
            {
                var serviceResponse = await service.GetAllAsync();
                return serviceResponse.ToApiResult();
            });

            group.MapGet("/{id:int}", async (int id, [FromServices] IRolesServices service) =>
            {
                var serviceResponse = await service.GetByIdAsync(id);
                return serviceResponse.ToApiResult();
            });

            group.MapPost("/", async (CreateRolesDto dto, [FromServices] IRolesServices service) =>
            {
                var serviceResponse = await service.CreateAsync(dto);
                return serviceResponse.ToApiResult();
            });

            group.MapPut("/{id:int}", async (int id, UpdateRolesDto dto, [FromServices] IRolesServices service) =>
            {
                var serviceResponse = await service.UpdateAsync(id, dto);
                return serviceResponse.ToApiResult();
            });

            group.MapDelete("/{id:int}", async (int id, [FromServices] IRolesServices service) =>
            {
                var serviceResponse = await service.DeleteAsync(id);
                return serviceResponse.ToApiResult();
            });

            return group;
        }
    }
}