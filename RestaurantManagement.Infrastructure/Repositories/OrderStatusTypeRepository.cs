using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderStatusTypeRepository : IOrderStatusTypeRepository
{
    private readonly AppDbContext _context;
    public OrderStatusTypeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<OrderStatusType>> GetAllAsync()
        => await _context.OrderStatusTypes.ToListAsync();

    public async Task<OrderStatusType?> GetByIdAsync(int id)
        => await _context.OrderStatusTypes.FindAsync(id);

    public async Task<OrderStatusType> AddAsync(OrderStatusType entity)
    {
        _context.OrderStatusTypes.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(OrderStatusType entity)
    {
        _context.OrderStatusTypes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.OrderStatusTypes.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false; // Soft delete
        _context.OrderStatusTypes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}