using ProductService.Domain.Entities;
using ProductService.Consumer;

namespace ProductService.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct = default);
		Task<Product?> GetProductByIdAsync(int id, CancellationToken ct = default);
		Task<Product> CreateProductAsync(Product product, CancellationToken ct = default);
		Task<Product?> UpdateProductAsync(int id, Product product, CancellationToken ct = default);
		Task<bool> DeleteProductAsync(int id, CancellationToken ct = default);
		Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category, CancellationToken ct = default);
		Task ProcessOrderMessageAsync(string message, CancellationToken ct = default);
		Task ProcessOrderViewedMessageAsync(OrderViewedMessage message, CancellationToken ct = default);
	}
}