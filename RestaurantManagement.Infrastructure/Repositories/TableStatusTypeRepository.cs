using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class TableStatusTypeRepository : ITableStatusTypeRepository
{
    private readonly AppDbContext _context;
    public TableStatusTypeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<TableStatusType>> GetAllAsync()
        => await _context.TableStatusTypes.Where( t => t.IsActive == true).ToListAsync();

    public async Task<TableStatusType?> GetByIdAsync(int id)
        => await _context.TableStatusTypes.FindAsync(id);

    public async Task<TableStatusType> AddAsync(TableStatusType entity)
    {
        _context.TableStatusTypes.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(TableStatusType entity)
    {
        _context.TableStatusTypes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.TableStatusTypes.FindAsync(id);
        if (entity == null) return false;
        // Soft delete: set IsActive to false instead of removing from database
        entity.IsActive = false;
        _context.TableStatusTypes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}