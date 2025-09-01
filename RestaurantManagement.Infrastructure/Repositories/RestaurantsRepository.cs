using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class RestaurantsRepository : IRestaurantsRepository
{
    private readonly AppDbContext _context;

    public RestaurantsRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Restaurants>> GetAllAsync()
        => await _context.Restaurants.ToListAsync();

    public async Task<Restaurants?> GetByIdAsync(Guid id)
        => await _context.Restaurants.FindAsync(id);

    public async Task AddAsync(Restaurants entity)
    {
        _context.Restaurants.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurants entity)
    {
        _context.Restaurants.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Restaurants.FindAsync(id);
        if (entity == null) return false;

        // Soft delete
        entity.M04IsActive = false;
        _context.Restaurants.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
