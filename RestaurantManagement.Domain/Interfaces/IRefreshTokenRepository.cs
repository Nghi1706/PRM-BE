using System;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Domain.Interfaces;
public interface IRefreshTokenRepository
{
    Task SaveAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task UpdateAsync(RefreshToken token);
}