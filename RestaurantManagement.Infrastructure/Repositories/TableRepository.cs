using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class TableRepository : ITableRepository
{
    private readonly AppDbContext _context;
    public TableRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<(Table Table, String StatusName)>> GetAllAsync(Guid roomId)
    {
        var query = _context.Tables
            .Where(t => t.RoomId == roomId)
            .Join(_context.TableStatusTypes,
                  t => t.TableStatusId,
                  s => s.Id,
                  (t, s) => new { Table = t, StatusName = s.Name });


        var result = await query.ToListAsync();
        return result.Select(x => (x.Table, x.StatusName));
    }

    //public async Task<IEnumerable<Table>> GetAllAsync(Guid roomId)
    //    => await _context.Tables.Where(t => t.RoomId == roomId && t.IsActive == true).ToListAsync();

    public async Task<Table?> GetByIdAsync(Guid id)
        => await _context.Tables.FindAsync(id);

    public async Task<Table> AddAsync(Table entity)
    {
        _context.Tables.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Table entity)
    {
        _context.Tables.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Tables.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;
        _context.Tables.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}