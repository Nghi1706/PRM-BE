using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ProductService.Services
{
	public class ProductAppService : IProductService
	{
		private readonly ILogger<ProductAppService> _logger;
		public ProductAppService(ILogger<ProductAppService> logger) => _logger = logger;

		public Task<IEnumerable<object>> GetProductsAsync(CancellationToken ct = default)
		{
			var products = new[]
			{
				new { Id = 1, Name = "Laptop", Price = 49.99m, Category = "Electronics" },
				new { Id = 2, Name = "Mouse",  Price = 29.99m, Category = "Electronics" },
				new { Id = 3, Name = "Keyboard", Price = 49.99m, Category = "Electronics" }
			};
			return Task.FromResult(products.AsEnumerable().Cast<object>());
		}

		public async Task ProcessOrderMessageAsync(string message, CancellationToken ct = default)
		{
			try
			{
				var doc = JsonDocument.Parse(message);
				var orderId = doc.RootElement.TryGetProperty("Id", out var idEl) ? idEl.ToString() : "unknown";
				_logger.LogInformation("[ProductService] Processing OrderCreated event: OrderId={OrderId}, Raw={Raw}", orderId, message);

				await Task.CompletedTask;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to process order message. Raw={Raw}", message);
				throw;
			}
		}
	}
}
