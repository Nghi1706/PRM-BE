using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IVoucherRepository
{
    Task<IEnumerable<Voucher>> GetAllAsync();
    Task<IEnumerable<Voucher>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<Voucher?> GetByIdAsync(Guid id);
    Task<Voucher?> GetByCodeAsync(string code);
    Task AddAsync(Voucher entity);
    Task UpdateAsync(Voucher entity);
    Task<bool> DeleteAsync(Guid id);
}