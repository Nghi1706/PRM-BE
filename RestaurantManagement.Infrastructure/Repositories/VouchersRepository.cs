using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Infrastructure.Data;

namespace RestaurantManagement.Infrastructure.Repositories;

public class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _context;

    public VoucherRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Voucher>> GetAllAsync()
        => await _context.Vouchers.ToListAsync();

    public async Task<IEnumerable<Voucher>> GetByRestaurantIdAsync(Guid restaurantId)
        => await _context.Vouchers.Where(v => v.M03RestaurantId == restaurantId).ToListAsync();

    public async Task<Voucher?> GetByIdAsync(Guid id)
        => await _context.Vouchers.FindAsync(id);

    public async Task<Voucher?> GetByCodeAsync(string code)
        => await _context.Vouchers.FirstOrDefaultAsync(v => v.M03Code == code);

    public async Task AddAsync(Voucher entity)
    {
        _context.Vouchers.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Voucher entity)
    {
        _context.Vouchers.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Vouchers.FindAsync(id);
        if (entity == null) return false;

        // Soft delete
        entity.M03IsActive = false;
        _context.Vouchers.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}