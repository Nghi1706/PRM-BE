using Common.Configurations;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly RabbitMqConfigHelper _rabbitMqHelper;
        private readonly IOptions<RabbitMqSettings> _settings;

        public OrderController(
            ILogger<OrderController> logger,
            RabbitMqConfigHelper rabbitMqHelper,
            IOptions<RabbitMqSettings> settings)
        {
            _logger = logger;
            _rabbitMqHelper = rabbitMqHelper;
            _settings = settings;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = new[]
            {
                new { Id = 1, CustomerId = 1, ProductId = 1, Quantity = 2, Total = 99.98m },
                new { Id = 2, CustomerId = 2, ProductId = 3, Quantity = 1, Total = 49.99m },
                new { Id = 3, CustomerId = 1, ProductId = 2, Quantity = 3, Total = 149.97m }
            };

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = new { Id = id, CustomerId = 1, ProductId = 1, Quantity = 2, Total = 99.98m };
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = new 
                { 
                    Id = Guid.NewGuid(),
                    CustomerId = request.CustomerId, 
                    ProductId = request.ProductId, 
                    Quantity = request.Quantity, 
                    Total = request.Quantity * 49.99m,
                    CreatedAt = DateTime.UtcNow
                };

                // Publish order created event
                var orderMessage = System.Text.Json.JsonSerializer.Serialize(order);
                await _rabbitMqHelper.PublishAsync(
                    _settings.Value.Exchange,
                    _settings.Value.RoutingKey,
                    orderMessage
                );

                _logger.LogInformation("Order created and published: {OrderId}", order.Id);

                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
        {
            var order = new { Id = id, CustomerId = request.CustomerId, ProductId = request.ProductId, Quantity = request.Quantity, Total = request.Quantity * 49.99m };
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            return NoContent();
        }
    }

    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    
}

