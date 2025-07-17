using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;
namespace RestaurantManagement.Infrastructure.Repositories;

public class DishRepository : IDishRepository
{
    private readonly AppDbContext _context;
    public DishRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Dish>> GetAllAsync(Guid restaurantId)
    {
        var query = _context.Dishes.AsQueryable();
        query = query.Where(d => d.RestaurantId == restaurantId && d.IsActive);
        return await query.ToListAsync();
    }


    public async Task<Dish?> GetByIdAsync(Guid id)
        => await _context.Dishes.FindAsync(id);

    public async Task<IEnumerable<Dish>> GetByIdCategoryAsync(Guid id)
    { var query = _context.Dishes.AsQueryable();
           query = query.Where(d => d.CategoryId == id && d.IsActive);
        return await query.ToListAsync();
    }

    public async Task AddAsync(Dish entity)
    {
        _context.Dishes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dish entity)
    {
        _context.Dishes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Dishes.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;
        _context.Dishes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}