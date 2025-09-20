using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure.Data
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly List<OrderItem> _orderItems;

        public OrderItemRepository()
        {
            _orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    ProductName = "Laptop Gaming",
                    UnitPrice = 1299.99m,
                    Quantity = 1,
                    TotalPrice = 1299.99m,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 2,
                    ProductName = "Wireless Mouse",
                    UnitPrice = 29.99m,
                    Quantity = 1,
                    TotalPrice = 29.99m,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 3,
                    ProductName = "Mechanical Keyboard",
                    UnitPrice = 89.99m,
                    Quantity = 1,
                    TotalPrice = 89.99m,
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 4,
                    OrderId = 3,
                    ProductId = 4,
                    ProductName = "Office Chair",
                    UnitPrice = 199.99m,
                    Quantity = 1,
                    TotalPrice = 199.99m,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false
                },
                new OrderItem
                {
                    Id = 5,
                    OrderId = 3,
                    ProductId = 5,
                    ProductName = "Coffee Mug",
                    UnitPrice = 12.99m,
                    Quantity = 1,
                    TotalPrice = 12.99m,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false
                }
            };
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            await Task.Delay(1); // Simulate async operation
            return _orderItems.Where(oi => oi.OrderId == orderId && !oi.IsDeleted).ToList();
        }

        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            await Task.Delay(1); // Simulate async operation
            orderItem.Id = _orderItems.Max(oi => oi.Id) + 1;
            orderItem.CreatedAt = DateTime.UtcNow;
            orderItem.IsDeleted = false;
            _orderItems.Add(orderItem);
            return orderItem;
        }

        public async Task<OrderItem> UpdateAsync(OrderItem orderItem)
        {
            await Task.Delay(1); // Simulate async operation
            var existingItem = _orderItems.FirstOrDefault(oi => oi.Id == orderItem.Id);
            if (existingItem != null)
            {
                existingItem.ProductId = orderItem.ProductId;
                existingItem.ProductName = orderItem.ProductName;
                existingItem.UnitPrice = orderItem.UnitPrice;
                existingItem.Quantity = orderItem.Quantity;
                existingItem.TotalPrice = orderItem.TotalPrice;
                existingItem.UpdatedAt = DateTime.UtcNow;
            }
            return orderItem;
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            var orderItem = _orderItems.FirstOrDefault(oi => oi.Id == id);
            if (orderItem != null)
            {
                orderItem.IsDeleted = true;
                orderItem.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task DeleteByOrderIdAsync(int orderId)
        {
            await Task.Delay(1); // Simulate async operation
            var orderItems = _orderItems.Where(oi => oi.OrderId == orderId).ToList();
            foreach (var item in orderItems)
            {
                item.IsDeleted = true;
                item.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
