using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Domain.Interfaces;
public interface ITableRepository
{
    Task<IEnumerable<(Table Table, String StatusName)>> GetAllAsync(Guid roomId);
    Task<Table?> GetByIdAsync(Guid id);
    Task<Table> AddAsync(Table entity);
    Task<bool> UpdateAsync(Table entity);
    Task<bool> DeleteAsync(Guid id);
}