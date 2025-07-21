using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync(Guid restaurantId);
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order> AddAsync(Order entity);
    Task<bool> UpdateAsync(Order entity);
    Task<bool> DeleteAsync(Guid id);
}