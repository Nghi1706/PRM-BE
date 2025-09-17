using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Application.Services
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly ITokenBlacklistStore _tokenBlacklistStore;

        public AuthApplication(IUserRepository userRepository, ITokenService tokenService, ITokenBlacklistStore tokenBlacklistStore)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _tokenBlacklistStore = tokenBlacklistStore;
        }

        public async Task<LoginResponse?> LoginAsync(UserLogin userLogin)
        {
            var user = await _userRepository.ValidateUserAsync(userLogin.Username, userLogin.Password);
            
            if (user == null)
            {
                return null;
            }

            var token = _tokenService.GenerateJwtToken(user);
            
            return new LoginResponse
            {
                Token = token,
                User = new UserInfo
                {
                    Username = user.Username,
                    Role = user.Role
                }
            };
        }

        public async Task<bool> LogoutAsync(string accessToken)
        {
            JwtSecurityToken token = _tokenService.GetJwtToken(accessToken);
            var expiry = token.ValidTo;

            if (DateTime.UtcNow < expiry)
            {
                await _tokenBlacklistStore.SetBlacklistAsync(accessToken, expiry);
            }
            return true;
        }
    }
}
