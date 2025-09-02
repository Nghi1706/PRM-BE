using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderDetailsRepository : IOrderDetailsRepository
{
    private readonly AppDbContext _context;

    public OrderDetailsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDetails>> GetAllAsync()
        => await _context.OrderDetails.ToListAsync();

    public async Task<IEnumerable<OrderDetails>> GetByOrderIdAsync(Guid orderId)
        => await _context.OrderDetails.Where(od => od.M10OrderId == orderId).ToListAsync();

    public async Task<OrderDetails?> GetByIdAsync(Guid id)
        => await _context.OrderDetails.FindAsync(id);

    public async Task<OrderDetails> AddAsync(OrderDetails entity)
    {
        _context.OrderDetails.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(OrderDetails entity)
    {
        _context.OrderDetails.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.OrderDetails.FindAsync(id);
        if (entity == null) return false;
        _context.OrderDetails.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}