using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto dto);
    Task<ServiceResponse<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);
    Task<ServiceResponse<object>> LogoutAsync(RefreshTokenRequestDto dto);
}