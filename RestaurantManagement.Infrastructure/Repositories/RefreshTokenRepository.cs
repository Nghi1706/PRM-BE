using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;
    public RefreshTokenRepository(AppDbContext context) => _context = context;

    public async Task SaveAsync(RefreshToken token)
    {
        _context.Set<RefreshToken>().Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
        => await _context.Set<RefreshToken>().FirstOrDefaultAsync(x => x.Token == token);

    public async Task UpdateAsync(RefreshToken token)
    {
        _context.Set<RefreshToken>().Update(token);
        await _context.SaveChangesAsync();
    }
}