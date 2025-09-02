using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IOrdersRepository
{
    Task<IEnumerable<Orders>> GetAllAsync();
    Task<IEnumerable<Orders>> GetByTableIdAsync(int tableId);
    Task<Orders?> GetByIdAsync(Guid id);
    Task AddAsync(Orders entity);
    Task UpdateAsync(Orders entity);
    Task<bool> DeleteAsync(Guid id);
}