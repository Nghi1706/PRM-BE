using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;
public interface IOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> GetAllAsync(Guid orderId);
    Task<OrderDetail?> GetByIdAsync(Guid id);
    Task<OrderDetail> AddAsync(OrderDetail entity);
    Task<bool> UpdateAsync(OrderDetail entity);
    Task<bool> DeleteAsync(Guid id);
}