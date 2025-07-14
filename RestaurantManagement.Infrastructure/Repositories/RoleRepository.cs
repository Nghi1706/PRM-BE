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
public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context) => _context = context;
    public async Task<IEnumerable<Role>> GetAllAsync()
        => await _context.Roles.ToListAsync();
    public async Task<Role?> GetByIdAsync(Guid id)
        => await _context.Roles.FindAsync(id);
    public async Task AddAsync(Role entity)
    {
        _context.Roles.Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Role entity)
    {
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Roles.FindAsync(id);
        if (entity == null) return false;
        entity.IsActive = false;
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}