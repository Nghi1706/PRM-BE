using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using Microsoft.Extensions.Options;
using RestaurantManagement.Application.Settings;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Common;
using BCrypt.Net;

namespace RestaurantManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsersRepository _userRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUsersRepository userRepository,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.M05HashedPW) || !user.M05IsActive)
            {
                return ServiceResponse<AuthResponseDto>.Unauthorized("Invalid credentials");
            }

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken(user);

            var userDto = new UsersDto
            {
                M05Id = user.M05Id,
                M05Name = user.M05Name,
                M05Email = user.M05Email,
                M05Phone = user.M05Phone,
                M05Avatar = user.M05Avatar,
                M05RoleId = user.M05RoleId,
                M05RestaurantId = user.M05RestaurantId,
                M05IsActive = user.M05IsActive,
                M05CreatedAt = user.M05CreatedAt,
                M05CreatedBy = user.M05CreatedBy,
                M05UpdatedAt = user.M05UpdatedAt,
                M05UpdatedBy = user.M05UpdatedBy
            };

            var response = new AuthResponseDto
            {
                User = userDto,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return ServiceResponse<AuthResponseDto>.Success(response, "Login successful");
        }
        catch (Exception ex)
        {
            return ServiceResponse<AuthResponseDto>.Error($"Error during login: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(dto.RefreshToken);
            if (principal == null)
            {
                return ServiceResponse<RefreshTokenResponseDto>.Unauthorized("Invalid refresh token");
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return ServiceResponse<RefreshTokenResponseDto>.Unauthorized("Invalid refresh token");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !user.M05IsActive)
            {
                return ServiceResponse<RefreshTokenResponseDto>.Unauthorized("User not found or inactive");
            }

            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken(user);

            var response = new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return ServiceResponse<RefreshTokenResponseDto>.Success(response, "Token refreshed successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<RefreshTokenResponseDto>.Error($"Error refreshing token: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> LogoutAsync(RefreshTokenRequestDto dto)
    {
        try
        {
            // Không cần xử lý gì vì không lưu token vào DB
            return ServiceResponse<object>.Success(null, "Logged out successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error during logout: {ex.Message}");
        }
    }

    private string GenerateAccessToken(Users user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.M05Id.ToString()),
            new Claim(ClaimTypes.Email, user.M05Email),
            new Claim("RestaurantId", user.M05RestaurantId.ToString()),
            new Claim(ClaimTypes.Role, user.M05RoleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.AccessTokenExpiryHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken(Users user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.M05Id.ToString()),
            new Claim("TokenType", "RefreshToken")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }
}