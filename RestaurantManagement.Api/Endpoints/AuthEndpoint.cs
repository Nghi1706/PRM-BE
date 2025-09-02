using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Api.Extensions;

namespace RestaurantManagement.Api.Endpoints;

public static class AuthEndpoint
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/login", async (LoginDto dto, [FromServices] IAuthService service) =>
        {
            var serviceResponse = await service.LoginAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/refresh", async (RefreshTokenRequestDto dto, [FromServices] IAuthService service) =>
        {
            var serviceResponse = await service.RefreshTokenAsync(dto);
            return serviceResponse.ToApiResult();
        });

        group.MapPost("/logout", async (RefreshTokenRequestDto dto, [FromServices] IAuthService service) =>
        {
            var serviceResponse = await service.LogoutAsync(dto);
            return serviceResponse.ToApiResult();
        });

        return group;
    }
}