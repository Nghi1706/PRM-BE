using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class DishesRepository : IDishesRepository
{
    private readonly AppDbContext _context;

    public DishesRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Dishes>> GetAllAsync()
        => await _context.Dishes.Where(d => d.M07IsActive == true).ToListAsync();

    public async Task<IEnumerable<Dishes>> GetByRestaurantIdAsync(Guid restaurantId)
        => await _context.Dishes.Where(d => d.M07RestaurantId == restaurantId && d.M07IsActive == true).ToListAsync();

    public async Task<IEnumerable<Dishes>> GetByCategoryIdAsync(int categoryId)
        => await _context.Dishes.Where(d => d.M07CategoryId == categoryId && d.M07IsActive == true).ToListAsync();

    public async Task<Dishes?> GetByIdAsync(int id)
        => await _context.Dishes.FindAsync(id);

    public async Task AddAsync(Dishes entity)
    {
        _context.Dishes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dishes entity)
    {
        _context.Dishes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Dishes.FindAsync(id);
        if (entity == null) return false;
        entity.M07IsActive = false; // Soft delete
        _context.Dishes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}