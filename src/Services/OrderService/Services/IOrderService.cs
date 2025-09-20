using OrderService.Domain.Entities;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken ct = default);
        Task<Order?> GetOrderByIdAsync(int id, CancellationToken ct = default);
        Task<Order?> GetOrderByNumberAsync(string orderNumber, CancellationToken ct = default);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId, CancellationToken ct = default);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status, CancellationToken ct = default);
        Task<Order> CreateOrderAsync(Order order, CancellationToken ct = default);
        Task<Order?> UpdateOrderAsync(int id, Order order, CancellationToken ct = default);
        Task<bool> DeleteOrderAsync(int id, CancellationToken ct = default);
        Task<bool> UpdateOrderStatusAsync(int id, string status, CancellationToken ct = default);
        Task<bool> ExistsOrderAsync(int id, CancellationToken ct = default);
    }
}
