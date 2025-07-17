using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Domain.Interfaces;
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    public CategoryRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync(Guid restaurantId)
        => await _context.Categories.Where(c => c.RestaurantId == restaurantId && c.IsActive).ToListAsync();

    public async Task<Category?> GetByIdAsync(Guid id)
        => await _context.Categories.FindAsync(id);

    public async Task AddAsync(Category entity)
    {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}