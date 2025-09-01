using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurants>> GetAllAsync();
    Task<Restaurants?> GetByIdAsync(Guid id);
    Task AddAsync(Restaurants entity);
    Task UpdateAsync(Restaurants entity);
    Task<bool> DeleteAsync(Guid id);
}