using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface ICategoriesRepository
{
    Task<IEnumerable<Categories>> GetAllAsync();
    Task<IEnumerable<Categories>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<Categories?> GetByIdAsync(int id);
    Task AddAsync(Categories entity);
    Task UpdateAsync(Categories entity);
    Task<bool> DeleteAsync(int id);
}