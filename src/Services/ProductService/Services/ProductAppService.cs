using Microsoft.Extensions.Logging;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Consumer;
using System.Text.Json;

namespace ProductService.Services
{
	public class ProductAppService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly ILogger<ProductAppService> _logger;

		public ProductAppService(IProductRepository productRepository, ILogger<ProductAppService> logger)
		{
			_productRepository = productRepository;
			_logger = logger;
		}

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Getting all products");
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Getting product by id: {Id}", id);
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product, CancellationToken ct = default)
        {
            _logger.LogInformation("Creating product: {Name}", product.Name);
            return await _productRepository.CreateAsync(product);
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product, CancellationToken ct = default)
        {
            _logger.LogInformation("Updating product: {Id}", id);
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            product.Id = id;
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Deleting product: {Id}", id);
            var exists = await _productRepository.ExistsAsync(id);
            if (!exists)
            {
                return false;
            }

            await _productRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category, CancellationToken ct = default)
        {
            _logger.LogInformation("Getting products by category: {Category}", category);
            return await _productRepository.GetByCategoryAsync(category);
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

        public async Task ProcessOrderViewedMessageAsync(OrderViewedMessage message, CancellationToken ct = default)
        {
            try
            {
                _logger.LogInformation("[ProductService] Processing OrderViewed event: OrderId={OrderId}, OrderNumber={OrderNumber}, CustomerId={CustomerId}, ProductIds={ProductIds}", 
                    message.OrderId, message.OrderNumber, message.CustomerId, string.Join(",", message.ProductIds));

                // Get product details for the viewed products
                var products = new List<Product>();
                foreach (var productId in message.ProductIds)
                {
                    var product = await _productRepository.GetByIdAsync(productId);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }

                _logger.LogInformation("[ProductService] Order viewed products: {ProductNames}", 
                    string.Join(", ", products.Select(p => p.Name)));

                // Here you can add business logic like:
                // - Update product view counts
                // - Track customer preferences
                // - Send recommendations
                // - Update analytics

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process order viewed message. OrderId={OrderId}", message.OrderId);
                throw;
            }
        }
    }
}