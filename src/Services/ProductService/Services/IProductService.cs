// src/Services/ProductService/Services/IProductService.cs
namespace ProductService.Services
{
	public interface IProductService
	{
		Task<IEnumerable<object>> GetProductsAsync(CancellationToken ct = default);
		Task ProcessOrderMessageAsync(string message, CancellationToken ct = default);
	}
}