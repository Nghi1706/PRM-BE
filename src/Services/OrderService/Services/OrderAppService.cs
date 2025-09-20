using Microsoft.Extensions.Logging;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Services
{
    public class OrderAppService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogger<OrderAppService> _logger;

        public OrderAppService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            ILogger<OrderAppService> logger)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Getting all orders");
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Getting order by id: {Id}", id);
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order?> GetOrderByNumberAsync(string orderNumber, CancellationToken ct = default)
        {
            _logger.LogInformation("Getting order by number: {OrderNumber}", orderNumber);
            return await _orderRepository.GetByOrderNumberAsync(orderNumber);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId, CancellationToken ct = default)
        {
            _logger.LogInformation("Getting orders by customer id: {CustomerId}", customerId);
            return await _orderRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status, CancellationToken ct = default)
        {
            _logger.LogInformation("Getting orders by status: {Status}", status);
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<Order> CreateOrderAsync(Order order, CancellationToken ct = default)
        {
            _logger.LogInformation("Creating order: {OrderNumber}", order.OrderNumber);
            
            // Generate order number if not provided
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                order.OrderNumber = GenerateOrderNumber();
            }

            // Calculate total amount
            order.TotalAmount = order.OrderItems.Sum(oi => oi.TotalPrice);

            return await _orderRepository.CreateAsync(order);
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order, CancellationToken ct = default)
        {
            _logger.LogInformation("Updating order: {Id}", id);
            
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return null;
            }

            // Update properties
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.CustomerEmail = order.CustomerEmail;
            existingOrder.Status = order.Status;
            existingOrder.ShippingAddress = order.ShippingAddress;
            existingOrder.Notes = order.Notes;

            // Update order items
            if (order.OrderItems.Any())
            {
                // Remove existing items
                await _orderItemRepository.DeleteByOrderIdAsync(id);
                
                // Add new items
                foreach (var item in order.OrderItems)
                {
                    item.OrderId = id;
                    await _orderItemRepository.CreateAsync(item);
                }
                
                // Recalculate total
                existingOrder.TotalAmount = order.OrderItems.Sum(oi => oi.TotalPrice);
            }

            return await _orderRepository.UpdateAsync(existingOrder);
        }

        public async Task<bool> DeleteOrderAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Deleting order: {Id}", id);
            
            var exists = await _orderRepository.ExistsAsync(id);
            if (!exists)
            {
                return false;
            }

            await _orderRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status, CancellationToken ct = default)
        {
            _logger.LogInformation("Updating order status: {Id} to {Status}", id, status);
            
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }

            order.Status = status;
            
            // Update dates based on status
            switch (status.ToLower())
            {
                case "shipped":
                    order.ShippedDate = DateTime.UtcNow;
                    break;
                case "delivered":
                    order.DeliveredDate = DateTime.UtcNow;
                    break;
            }

            await _orderRepository.UpdateAsync(order);
            return true;
        }

        public async Task<bool> ExistsOrderAsync(int id, CancellationToken ct = default)
        {
            return await _orderRepository.ExistsAsync(id);
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }
    }
}
