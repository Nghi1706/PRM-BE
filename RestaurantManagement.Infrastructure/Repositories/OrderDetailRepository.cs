using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly AppDbContext _context;
    public OrderDetailRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<OrderDetail>> GetAllAsync(Guid orderId)
        => await _context.OrderDetails
            .Where(od => od.OrderId == orderId)
            .ToListAsync();

    public async Task<OrderDetail?> GetByIdAsync(Guid id)
        => await _context.OrderDetails.FindAsync(id);

    public async Task<OrderDetail> AddAsync(OrderDetail entity)
    {
        _context.OrderDetails.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(OrderDetail entity)
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