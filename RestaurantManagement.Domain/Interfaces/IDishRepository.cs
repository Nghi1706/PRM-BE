using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface IDishRepository
{
    Task<IEnumerable<Dish>> GetAllAsync(Guid restaurantId);
    Task<Dish?> GetByIdAsync(Guid id);
    Task<IEnumerable<Dish>> GetByIdCategoryAsync(Guid id);
    Task AddAsync(Dish entity);
    Task UpdateAsync(Dish entity);
    Task<bool> DeleteAsync(Guid id);
}