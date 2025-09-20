using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders;

        public OrderRepository()
        {
            _orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    OrderNumber = "ORD-20241201-ABC12345",
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    CustomerEmail = "john@example.com",
                    TotalAmount = 149.98m,
                    Status = "Delivered",
                    OrderDate = DateTime.UtcNow.AddDays(-10),
                    ShippedDate = DateTime.UtcNow.AddDays(-8),
                    DeliveredDate = DateTime.UtcNow.AddDays(-5),
                    ShippingAddress = "123 Main St, City, State 12345",
                    Notes = "Please deliver during business hours",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false,
                    OrderItems = new List<OrderItem>
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
                        }
                    }
                },
                new Order
                {
                    Id = 2,
                    OrderNumber = "ORD-20241202-DEF67890",
                    CustomerId = 2,
                    CustomerName = "Jane Smith",
                    CustomerEmail = "jane@example.com",
                    TotalAmount = 89.99m,
                    Status = "Shipped",
                    OrderDate = DateTime.UtcNow.AddDays(-5),
                    ShippedDate = DateTime.UtcNow.AddDays(-2),
                    ShippingAddress = "456 Oak Ave, City, State 67890",
                    Notes = "Fragile items",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    IsDeleted = false,
                    OrderItems = new List<OrderItem>
                    {
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
                        }
                    }
                },
                new Order
                {
                    Id = 3,
                    OrderNumber = "ORD-20241203-GHI11111",
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    CustomerEmail = "john@example.com",
                    TotalAmount = 212.98m,
                    Status = "Pending",
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    ShippingAddress = "123 Main St, City, State 12345",
                    Notes = "Rush order",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false,
                    OrderItems = new List<OrderItem>
                    {
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
                    }
                }
            };
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.FirstOrDefault(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<Order?> GetByOrderNumberAsync(string orderNumber)
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.FirstOrDefault(o => o.OrderNumber == orderNumber && !o.IsDeleted);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.Where(o => !o.IsDeleted).OrderByDescending(o => o.OrderDate).ToList();
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.Where(o => o.CustomerId == customerId && !o.IsDeleted).OrderByDescending(o => o.OrderDate).ToList();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.Where(o => o.Status == status && !o.IsDeleted).OrderByDescending(o => o.OrderDate).ToList();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await Task.Delay(1); // Simulate async operation
            order.Id = _orders.Max(o => o.Id) + 1;
            order.CreatedAt = DateTime.UtcNow;
            order.IsDeleted = false;
            _orders.Add(order);
            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            await Task.Delay(1); // Simulate async operation
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.CustomerName = order.CustomerName;
                existingOrder.CustomerEmail = order.CustomerEmail;
                existingOrder.Status = order.Status;
                existingOrder.ShippingAddress = order.ShippingAddress;
                existingOrder.Notes = order.Notes;
                existingOrder.UpdatedAt = DateTime.UtcNow;
            }
            return order;
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.IsDeleted = true;
                order.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.Any(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<bool> ExistsByOrderNumberAsync(string orderNumber)
        {
            await Task.Delay(1); // Simulate async operation
            return _orders.Any(o => o.OrderNumber == orderNumber && !o.IsDeleted);
        }
    }
}
