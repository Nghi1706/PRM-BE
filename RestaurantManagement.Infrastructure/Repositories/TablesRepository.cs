using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class TablesRepository : ITablesRepository
{
    private readonly AppDbContext _context;

    public TablesRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Tables>> GetAllAsync()
        => await _context.Tables.Where(t => t.M08IsActive).ToListAsync();

    public async Task<IEnumerable<Tables>> GetByRestaurantIdAsync(Guid restaurantId)
        => await _context.Tables.Where(t => t.M08RestaurantId == restaurantId && t.M08IsActive).ToListAsync();

    public async Task<Tables?> GetByIdAsync(int id)
        => await _context.Tables.FindAsync(id);

    public async Task AddAsync(Tables entity)
    {
        _context.Tables.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tables entity)
    {
        _context.Tables.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Tables.FindAsync(id);
        if (entity == null) return false;

        // Soft delete
        entity.M08IsActive = false;
        entity.M08UpdatedAt = DateTime.UtcNow;
        _context.Tables.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}