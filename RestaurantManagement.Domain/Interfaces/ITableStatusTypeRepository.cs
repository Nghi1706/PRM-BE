using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface ITableStatusTypeRepository
{
    Task<IEnumerable<TableStatusType>> GetAllAsync();
    Task<TableStatusType?> GetByIdAsync(int id);
    Task<TableStatusType> AddAsync(TableStatusType entity);
    Task<bool> UpdateAsync(TableStatusType entity);
    Task<bool> DeleteAsync(int id);
}