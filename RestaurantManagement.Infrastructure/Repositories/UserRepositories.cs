using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
            => await _context.Users.ToListAsync();
        public async Task<User?> GetByIdAsync(Guid id)
            => await _context.Users.FindAsync(id);
        public async Task<User?> AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null) return false;
            entity.IsActive = false;
            _context.Users.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
