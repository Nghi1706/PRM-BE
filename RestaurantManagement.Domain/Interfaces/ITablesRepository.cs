using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface ITablesRepository
{
    Task<IEnumerable<Tables>> GetAllAsync();
    Task<IEnumerable<Tables>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<Tables?> GetByIdAsync(int id);
    Task AddAsync(Tables entity);
    Task UpdateAsync(Tables entity);
    Task<bool> DeleteAsync(int id);
}