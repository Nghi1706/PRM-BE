using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IDishesRepository
{
    Task<IEnumerable<Dishes>> GetAllAsync();
    Task<IEnumerable<Dishes>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<IEnumerable<Dishes>> GetByCategoryIdAsync(int categoryId);
    Task<Dishes?> GetByIdAsync(int id);
    Task AddAsync(Dishes entity);
    Task UpdateAsync(Dishes entity);
    Task<bool> DeleteAsync(int id);
}