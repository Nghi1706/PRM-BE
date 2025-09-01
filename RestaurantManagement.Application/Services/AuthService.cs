using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using Microsoft.Extensions.Options;
using RestaurantManagement.Application.Settings;

namespace RestaurantManagement.Application.Services;

public class AuthService
{
    private readonly IUsersRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUsersRepository userRepository, 
        IRefreshTokenRepository refreshTokenRepository, 
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<(string accessToken, string refreshToken)?> LoginAsync(LoginDto dto)
    {
        var user = (await _userRepository.GetAllAsync())
            .FirstOrDefault(u => u.M05Email == dto.Email && 
                          BCrypt.Net.BCrypt.Verify(dto.Password, u.M05HashedPW) && 
                          u.M05IsActive == true);

        if (user == null) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.M05Id.ToString()),
            new Claim(ClaimTypes.Email, user.M05Email),
            new Claim("RestaurantId", user.M05RestaurantId.ToString()),
            new Claim(ClaimTypes.Role, user.M05RoleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.AccessTokenExpiryHours),
            signingCredentials: creds
        );

        var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

        // Create refresh token
        var refreshToken = Guid.NewGuid().ToString();
        await _refreshTokenRepository.SaveAsync(new RefreshToken
        {
            UserId = user.M05Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
            Revoked = false
        });

        return (accessTokenString, refreshToken);
    }

    public async Task<string?> RefreshTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (token == null || token.ExpiresAt < DateTime.UtcNow || token.Revoked)
            return null;

        var user = await _userRepository.GetByIdAsync(token.UserId);
        if (user == null) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.M05Id.ToString()),
            new Claim(ClaimTypes.Email, user.M05Email),
            new Claim("RestaurantId", user.M05RestaurantId.ToString()),
            new Claim(ClaimTypes.Role, user.M05RoleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.AccessTokenExpiryHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(accessToken);
    }

    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (token == null) return false;
        token.Revoked = true;
        await _refreshTokenRepository.UpdateAsync(token);
        return true;
    }
}