using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IStatusRepository
{
    Task<IEnumerable<Status>> GetAllAsync();
    Task<Status> GetByIdAsync(Guid id);
    Task AddAsync(Status entity);
    Task UpdateAsync(Status entity);
    Task<bool> DeleteAsync(Guid id);
}
