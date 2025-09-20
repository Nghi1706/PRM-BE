using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop Gaming",
                    Description = "High-performance gaming laptop",
                    Price = 1299.99m,
                    Stock = 50,
                    Category = "Electronics",
                    ImageUrl = "https://example.com/laptop.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse",
                    Price = 29.99m,
                    Stock = 100,
                    Category = "Electronics",
                    ImageUrl = "https://example.com/mouse.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Mechanical Keyboard",
                    Description = "RGB mechanical keyboard",
                    Price = 89.99m,
                    Stock = 75,
                    Category = "Electronics",
                    ImageUrl = "https://example.com/keyboard.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "Office Chair",
                    Description = "Comfortable office chair",
                    Price = 199.99m,
                    Stock = 25,
                    Category = "Furniture",
                    ImageUrl = "https://example.com/chair.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Coffee Mug",
                    Description = "Ceramic coffee mug",
                    Price = 12.99m,
                    Stock = 200,
                    Category = "Accessories",
                    ImageUrl = "https://example.com/mug.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    IsDeleted = false
                }
            };
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            return _products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            await Task.Delay(1); // Simulate async operation
            return _products.Where(p => p.IsActive && !p.IsDeleted).ToList();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            await Task.Delay(1); // Simulate async operation
            return _products.Where(p => p.Category == category && p.IsActive && !p.IsDeleted).ToList();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await Task.Delay(1); // Simulate async operation
            product.Id = _products.Max(p => p.Id) + 1;
            product.CreatedAt = DateTime.UtcNow;
            product.IsDeleted = false;
            _products.Add(product);
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            await Task.Delay(1); // Simulate async operation
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.Category = product.Category;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.IsActive = product.IsActive;
                existingProduct.UpdatedAt = DateTime.UtcNow;
            }
            return product;
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsDeleted = true;
                product.UpdatedAt = DateTime.UtcNow;
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            return _products.Any(p => p.Id == id && !p.IsDeleted);
        }
    }
}
