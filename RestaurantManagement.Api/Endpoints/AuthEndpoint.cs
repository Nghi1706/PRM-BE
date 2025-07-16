using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Services;

public static class AuthEndpoint
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/login", async (LoginDto dto, [FromServices] AuthService authService) =>
        {
            var result = await authService.LoginAsync(dto);
            if (result == null)
                return Results.Unauthorized();

            return Results.Ok(new { accessToken = result.Value.accessToken, refreshToken = result.Value.refreshToken });
        });

        group.MapPost("/refresh", async ([FromBody] string refreshToken, [FromServices] AuthService authService) =>
        {
            var newAccessToken = await authService.RefreshTokenAsync(refreshToken);
            if (newAccessToken == null)
                return Results.Unauthorized();

            return Results.Ok(new { accessToken = newAccessToken });
        });

        group.MapPost("/logout", async ([FromBody] string refreshToken, [FromServices] AuthService authService) =>
        {
            var result = await authService.LogoutAsync(refreshToken);
            return result ? Results.Ok() : Results.BadRequest();
        });

        return group;
    }
}