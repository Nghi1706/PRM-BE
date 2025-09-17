using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Domain.Interfaces
{
    public interface ITokenBlacklistStore
    {
        Task SetBlacklistAsync(string token, DateTimeOffset expiresAt);
    }
}
