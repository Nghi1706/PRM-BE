using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Infrastructure.Data;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagement.Infrastructure.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly AppDbContext _context;
    public RolesRepository(AppDbContext context) => _context = context;
    public async Task<IEnumerable<Roles>> GetAllAsync()
        => await _context.Roles.ToListAsync();
    public async Task<Roles?> GetByIdAsync(int id)
        => await _context.Roles.FindAsync(id);
    public async Task AddAsync(Roles entity)
    {
        _context.Roles.Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Roles entity)
    {
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Roles.FindAsync(id);
        if (entity == null) return false;
        entity.M01IsActive = false;
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}