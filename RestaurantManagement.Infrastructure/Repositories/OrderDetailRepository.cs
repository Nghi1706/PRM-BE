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
        .Join(_context.Dishes,
                od => od.DishId,
                d => d.Id,
                (od, d) => new {od, d})
            .Join(_context.DishStatusTypes, 
            od => od.od.DishStatusId,
            dst => dst.Id,
            (od, dst) => new OrderDetail
            {
                Id = od.od.Id,
                OrderId = od.od.OrderId,
                DishId = od.od.DishId,
                DishName = od.d.Name,
                Quantity = od.od.Quantity,
                Price = od.od.Price,
                DishStatusId = dst.Id,
                DishStatusName = dst.Name,
                CreatedAt = od.od.CreatedAt,
                CreatedByUser = od.od.CreatedByUser,
                UpdatedAt = od.od.UpdatedAt,
                UpdatedByUser = od.od.UpdatedByUser
            })
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