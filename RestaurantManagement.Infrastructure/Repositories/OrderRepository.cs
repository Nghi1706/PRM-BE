using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Order>> GetAllAsync(Guid restaurantId)
        => await _context.Orders
            .Where(o => o.RestaurantId == restaurantId)
            .ToListAsync();
    public async Task<IEnumerable<Order>> GetByRoomAsync(Guid roomId)
        => await _context.Orders
            .Where(o => o.RoomId == roomId)
            .ToListAsync();
    public async Task<IEnumerable<Order>> GetByTableAsync(Guid tableId)
        => await _context.Orders
            .Where(o => o.TableId == tableId)
            .ToListAsync();

    public async Task<Order?> GetByIdAsync(Guid id)
        => await _context.Orders.FindAsync(id);

    public async Task<Order> AddAsync(Order entity)
    {
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
        return true;
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