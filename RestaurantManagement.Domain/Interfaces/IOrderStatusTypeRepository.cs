using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface IDishStatusTypeRepository
{
    Task<IEnumerable<DishStatusType>> GetAllAsync();
    Task<DishStatusType?> GetByIdAsync(int id);
    Task<DishStatusType> AddAsync(DishStatusType entity);
    Task<bool> UpdateAsync(DishStatusType entity);
    Task<bool> DeleteAsync(int id);
}