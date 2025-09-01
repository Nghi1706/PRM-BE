using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Interfaces;

public interface IVouchersService
{
    Task<ServiceResponse<IEnumerable<VoucherDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<VoucherDto>>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<ServiceResponse<VoucherDto>> GetByIdAsync(Guid id);
    Task<ServiceResponse<VoucherDto>> GetByCodeAsync(string code);
    Task<ServiceResponse<VoucherDto>> CreateAsync(CreateVoucherDto dto);
    Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateVoucherDto dto);
    Task<ServiceResponse<object>> DeleteAsync(Guid id);
}