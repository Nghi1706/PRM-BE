using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync(Guid restaurantId);
    Task<Category?> GetByIdAsync(Guid id);
    Task AddAsync(Category entity);
    Task UpdateAsync(Category entity);
    Task<bool> DeleteAsync(Guid id);
}