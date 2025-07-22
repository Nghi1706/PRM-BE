using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class DishStatusTypeRepository : IDishStatusTypeRepository
{
    private readonly AppDbContext _context;
    public DishStatusTypeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<DishStatusType>> GetAllAsync()
        => await _context.DishStatusTypes.ToListAsync();

    public async Task<DishStatusType?> GetByIdAsync(int id)
        => await _context.DishStatusTypes.FindAsync(id);

    public async Task<DishStatusType> AddAsync(DishStatusType entity)
    {
        _context.DishStatusTypes.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(DishStatusType entity)
    {
        _context.DishStatusTypes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.DishStatusTypes.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false; // Soft delete
        _context.DishStatusTypes.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}