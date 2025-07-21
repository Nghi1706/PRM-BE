using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface ITableSessionRepository
{
    Task<IEnumerable<TableSession>> GetAllAsync(Guid tableId);
    Task<TableSession?> GetByIdAsync(Guid id);
    Task<TableSession> AddAsync(TableSession entity);
    Task<bool> UpdateAsync(TableSession entity);
    Task<bool> DeleteAsync(Guid id);
}