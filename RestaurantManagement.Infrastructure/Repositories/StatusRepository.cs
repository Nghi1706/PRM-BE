using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly AppDbContext _context;

    public StatusRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Status>> GetAllAsync()
        => await _context.Status.ToListAsync();

    public async Task<Status?> GetByIdAsync(int id)
        => await _context.Status.FindAsync(id);

    public async Task AddAsync(Status entity)
    {
        _context.Status.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Status entity)
    {
        _context.Status.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Status.FindAsync(id);
        if (entity == null) return false;

        // Soft delete
        entity.M02IsActive = false;
        _context.Status.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
