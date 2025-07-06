using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IStatusCategoryRepository
{
    Task<IEnumerable<StatusCategory>> GetAllAsync();
    Task<StatusCategory?> GetByIdAsync(Guid id);
    Task AddAsync(StatusCategory entity);
    Task UpdateAsync(StatusCategory entity);
    Task<bool> DeleteAsync(Guid id);
}
