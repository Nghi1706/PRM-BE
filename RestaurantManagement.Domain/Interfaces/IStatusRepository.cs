using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IStatusRepository
{
    Task<IEnumerable<Status>> GetAllAsync();
    Task<Status?> GetByIdAsync(int id);
    Task AddAsync(Status entity);
    Task UpdateAsync(Status entity);
    Task<bool> DeleteAsync(int id);
}
