using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IOrderDetailsRepository
{
    Task<IEnumerable<OrderDetails>> GetAllAsync();
    Task<IEnumerable<OrderDetails>> GetByOrderIdAsync(Guid orderId);
    Task<OrderDetails?> GetByIdAsync(Guid id);
    Task<OrderDetails> AddAsync(OrderDetails entity);
    Task<bool> UpdateAsync(OrderDetails entity);
    Task<bool> DeleteAsync(Guid id);
}