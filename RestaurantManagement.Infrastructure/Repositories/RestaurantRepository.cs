using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
        => await _context.Restaurants.ToListAsync();

    public async Task<Restaurant?> GetByIdAsync(Guid id)
        => await _context.Restaurants.FindAsync(id);

    public async Task AddAsync(Restaurant entity)
    {
        _context.Restaurants.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurant entity)
    {
        _context.Restaurants.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Restaurants.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;

        _context.Restaurants.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
