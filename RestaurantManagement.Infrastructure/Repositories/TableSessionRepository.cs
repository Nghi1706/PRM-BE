using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class TableSessionRepository : ITableSessionRepository
{
    private readonly AppDbContext _context;
    public TableSessionRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<TableSession>> GetAllAsync(Guid tableId)
        => await _context.TableSessions
            .Where(ts => ts.TableId == tableId)
            .ToListAsync();

    public async Task<TableSession?> GetByIdAsync(Guid id)
        => await _context.TableSessions.FindAsync(id);

    public async Task<TableSession> AddAsync(TableSession entity)
    {
        _context.TableSessions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(TableSession entity)
    {
        _context.TableSessions.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.TableSessions.FindAsync(id);
        if (entity == null) return false;
        _context.TableSessions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}