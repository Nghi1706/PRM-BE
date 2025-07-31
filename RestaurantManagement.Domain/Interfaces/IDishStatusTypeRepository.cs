using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface IOrderStatusTypeRepository
{
    Task<IEnumerable<OrderStatusType>> GetAllAsync();
    Task<OrderStatusType?> GetByIdAsync(int id);
    Task<OrderStatusType> AddAsync(OrderStatusType entity);
    Task<bool> UpdateAsync(OrderStatusType entity);
    Task<bool> DeleteAsync(int id);
}