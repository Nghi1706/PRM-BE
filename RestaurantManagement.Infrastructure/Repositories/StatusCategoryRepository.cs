using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class StatusCategoryRepository : IStatusCategoryRepository
{
    private readonly AppDbContext _context;

    public StatusCategoryRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<StatusCategory>> GetAllAsync()
        => await _context.StatusCategories.ToListAsync();

    public async Task<StatusCategory?> GetByIdAsync(Guid id)
        => await _context.StatusCategories.FindAsync(id);

    public async Task AddAsync(StatusCategory entity)
    {
        _context.StatusCategories.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StatusCategory entity)
    {
        _context.StatusCategories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.StatusCategories.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;

        _context.StatusCategories.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
