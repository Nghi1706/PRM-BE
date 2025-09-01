using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly AppDbContext _context;

    public CategoriesRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Categories>> GetAllAsync()
        => await _context.Categories.Where(c => c.M06IsActive == true).ToListAsync();

    public async Task<IEnumerable<Categories>> GetByRestaurantIdAsync(Guid restaurantId)
        => await _context.Categories.Where(c => c.M06RestaurantId == restaurantId && c.M06IsActive == true).ToListAsync();

    public async Task<Categories?> GetByIdAsync(int id)
        => await _context.Categories.FindAsync(id);

    public async Task AddAsync(Categories entity)
    {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Categories entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Categories.FindAsync(id);
        if (entity == null) return false;

        // Soft delete
        entity.M06IsActive = false;
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}