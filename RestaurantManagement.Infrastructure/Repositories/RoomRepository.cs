using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;
    public RoomRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Room>> GetAllAsync(Guid restaurantId)
        => await _context.Rooms.Where(r => r.RestaurantId == restaurantId && r.IsActive == true).ToListAsync();

    public async Task<Room?> GetByIdAsync(Guid id)
        => await _context.Rooms.FindAsync(id);

    public async Task<Room> AddAsync(Room entity)
    {
        _context.Rooms.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Room entity)
    {
        _context.Rooms.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Rooms.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;
        _context.Rooms.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}