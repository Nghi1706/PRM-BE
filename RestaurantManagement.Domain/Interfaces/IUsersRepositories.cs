using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IUsersRepository
{
    Task<IEnumerable<Users>> GetAllAsync();
    Task<IEnumerable<Users>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<Users?> GetByIdAsync(Guid id);
    Task<Users?> GetByEmailAsync(string email);
    Task AddAsync(Users entity);
    Task UpdateAsync(Users entity);
    Task<bool> DeleteAsync(Guid id);
}
