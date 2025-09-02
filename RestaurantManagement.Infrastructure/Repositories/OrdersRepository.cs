using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Orders>> GetAllAsync()
        => await _context.Orders.ToListAsync();

    public async Task<IEnumerable<Orders>> GetByTableIdAsync(int tableId)
        => await _context.Orders.Where(o => o.M09TableId == tableId).ToListAsync();

    public async Task<Orders?> GetByIdAsync(Guid id)
        => await _context.Orders.FindAsync(id);

    public async Task AddAsync(Orders entity)
    {
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Orders entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Orders.FindAsync(id);
        if (entity == null) return false;

        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}