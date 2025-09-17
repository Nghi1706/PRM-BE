using AuthService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        JwtSecurityToken GetJwtToken(string accessToken);
    }
}
