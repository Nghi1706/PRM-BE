using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<LoginResponse?> LoginAsync(UserLogin userLogin);
        Task<bool> LogoutAsync(string accessToken);
    }
}
