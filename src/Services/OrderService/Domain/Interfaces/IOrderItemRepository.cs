using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<OrderItem> CreateAsync(OrderItem orderItem);
        Task<OrderItem> UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(int id);
        Task DeleteByOrderIdAsync(int orderId);
    }
}
