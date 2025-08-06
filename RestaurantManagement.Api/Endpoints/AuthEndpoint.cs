using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Services;
using RestaurantManagement.Shared.Response;
using System.Net;

namespace RestaurantManagement.Api.Endpoints;

public static class AuthEndpoint
{
    public static RouteGroupBuilder MapAuthEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/login", async (LoginDto dto, [FromServices] AuthService authService) =>
        {
            var result = await authService.LoginAsync(dto);
            if (result == null)
            {
                var unauthorizedResponse = ApiResponse.Unauthorized("Invalid credentials");
                return Results.Json(unauthorizedResponse, statusCode: unauthorizedResponse.StatusCode);
            }

            var response = ApiResponse.Success(
                new { access_token = result.Value.accessToken, refresh_token = result.Value.refreshToken },
                "Login successful");

            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPost("/refresh", async ([FromBody] string refreshToken, [FromServices] AuthService authService) =>
        {
            var newAccessToken = await authService.RefreshTokenAsync(refreshToken);
            if (newAccessToken == null)
            {
                var unauthorizedResponse = ApiResponse.Unauthorized("Invalid refresh token");
                return Results.Json(unauthorizedResponse, statusCode: unauthorizedResponse.StatusCode);
            }

            var response = ApiResponse.Success(
                new { accessToken = newAccessToken },
                "Token refreshed successfully");

            return Results.Json(response, statusCode: response.StatusCode);
        });

        group.MapPost("/logout", async ([FromBody] string refreshToken, [FromServices] AuthService authService) =>
        {
            var result = await authService.LogoutAsync(refreshToken);

            if (result)
            {
                var response = ApiResponse.Success(null, "Logged out successfully");
                return Results.Json(response, statusCode: response.StatusCode);
            }
            else
            {
                var errorResponse = ApiResponse.Failure("Failed to logout");
                return Results.Json(errorResponse, statusCode: errorResponse.StatusCode);
            }
        });

        return group;
    }
}