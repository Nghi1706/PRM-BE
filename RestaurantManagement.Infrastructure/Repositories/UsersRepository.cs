using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class UserRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Users>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task<IEnumerable<Users>> GetByRestaurantIdAsync(Guid restaurantId)
        => await _context.Users.Where(u => u.M05RestaurantId == restaurantId).ToListAsync();

    public async Task<Users?> GetByIdAsync(Guid id)
        => await _context.Users.FindAsync(id);

    public async Task<Users?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.M05Email == email);

    public async Task AddAsync(Users entity)
    {
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Users entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null) return false;

        // Soft delete
        entity.M05IsActive = false;
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
