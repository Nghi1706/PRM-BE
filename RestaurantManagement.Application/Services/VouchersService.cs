using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Services;

public class VouchersService : IVouchersService
{
    private readonly IVoucherRepository _voucherRepository;

    public VouchersService(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<ServiceResponse<IEnumerable<VoucherDto>>> GetAllAsync()
    {
        try
        {
            var vouchers = await _voucherRepository.GetAllAsync();
            var voucherDtos = vouchers.Select(voucher => new VoucherDto
            {
                M03Id = voucher.M03Id,
                M03Code = voucher.M03Code,
                M03Description = voucher.M03Description,
                M03DiscountValue = voucher.M03DiscountValue,
                M03IsActive = voucher.M03IsActive,
                M03MinOrderValue = voucher.M03MinOrderValue,
                M03MaxDiscount = voucher.M03MaxDiscount,
                M03FromDate = voucher.M03FromDate,
                M03ToDate = voucher.M03ToDate,
                M03UsageLimit = voucher.M03UsageLimit,
                M03RestaurantId = voucher.M03RestaurantId,
                M03CreatedAt = voucher.M03CreatedAt,
                M03CreatedBy = voucher.M03CreatedBy,
                M03UpdatedAt = voucher.M03UpdatedAt,
                M03UpdatedBy = voucher.M03UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<VoucherDto>>.Success(voucherDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<VoucherDto>>.Error($"Error retrieving vouchers: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<VoucherDto>>> GetByRestaurantIdAsync(Guid restaurantId)
    {
        try
        {
            var vouchers = await _voucherRepository.GetByRestaurantIdAsync(restaurantId);
            var voucherDtos = vouchers.Select(voucher => new VoucherDto
            {
                M03Id = voucher.M03Id,
                M03Code = voucher.M03Code,
                M03Description = voucher.M03Description,
                M03DiscountValue = voucher.M03DiscountValue,
                M03IsActive = voucher.M03IsActive,
                M03MinOrderValue = voucher.M03MinOrderValue,
                M03MaxDiscount = voucher.M03MaxDiscount,
                M03FromDate = voucher.M03FromDate,
                M03ToDate = voucher.M03ToDate,
                M03UsageLimit = voucher.M03UsageLimit,
                M03RestaurantId = voucher.M03RestaurantId,
                M03CreatedAt = voucher.M03CreatedAt,
                M03CreatedBy = voucher.M03CreatedBy,
                M03UpdatedAt = voucher.M03UpdatedAt,
                M03UpdatedBy = voucher.M03UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<VoucherDto>>.Success(voucherDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<VoucherDto>>.Error($"Error retrieving vouchers: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<VoucherDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            if (voucher == null)
            {
                return ServiceResponse<VoucherDto>.NotFound("Voucher not found");
            }

            var voucherDto = new VoucherDto
            {
                M03Id = voucher.M03Id,
                M03Code = voucher.M03Code,
                M03Description = voucher.M03Description,
                M03DiscountValue = voucher.M03DiscountValue,
                M03IsActive = voucher.M03IsActive,
                M03MinOrderValue = voucher.M03MinOrderValue,
                M03MaxDiscount = voucher.M03MaxDiscount,
                M03FromDate = voucher.M03FromDate,
                M03ToDate = voucher.M03ToDate,
                M03UsageLimit = voucher.M03UsageLimit,
                M03RestaurantId = voucher.M03RestaurantId,
                M03CreatedAt = voucher.M03CreatedAt,
                M03CreatedBy = voucher.M03CreatedBy,
                M03UpdatedAt = voucher.M03UpdatedAt,
                M03UpdatedBy = voucher.M03UpdatedBy
            };

            return ServiceResponse<VoucherDto>.Success(voucherDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<VoucherDto>.Error($"Error retrieving voucher: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<VoucherDto>> GetByCodeAsync(string code)
    {
        try
        {
            var voucher = await _voucherRepository.GetByCodeAsync(code);
            if (voucher == null)
            {
                return ServiceResponse<VoucherDto>.NotFound("Voucher not found");
            }

            var voucherDto = new VoucherDto
            {
                M03Id = voucher.M03Id,
                M03Code = voucher.M03Code,
                M03Description = voucher.M03Description,
                M03DiscountValue = voucher.M03DiscountValue,
                M03IsActive = voucher.M03IsActive,
                M03MinOrderValue = voucher.M03MinOrderValue,
                M03MaxDiscount = voucher.M03MaxDiscount,
                M03FromDate = voucher.M03FromDate,
                M03ToDate = voucher.M03ToDate,
                M03UsageLimit = voucher.M03UsageLimit,
                M03RestaurantId = voucher.M03RestaurantId,
                M03CreatedAt = voucher.M03CreatedAt,
                M03CreatedBy = voucher.M03CreatedBy,
                M03UpdatedAt = voucher.M03UpdatedAt,
                M03UpdatedBy = voucher.M03UpdatedBy
            };

            return ServiceResponse<VoucherDto>.Success(voucherDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<VoucherDto>.Error($"Error retrieving voucher: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<VoucherDto>> CreateAsync(CreateVoucherDto dto)
    {
        try
        {
            var voucher = new Voucher
            {
                M03Code = dto.M03Code,
                M03Description = dto.M03Description,
                M03DiscountValue = dto.M03DiscountValue,
                M03IsActive = dto.M03IsActive,
                M03MinOrderValue = dto.M03MinOrderValue,
                M03MaxDiscount = dto.M03MaxDiscount,
                M03FromDate = dto.M03FromDate,
                M03ToDate = dto.M03ToDate,
                M03UsageLimit = dto.M03UsageLimit,
                M03RestaurantId = dto.M03RestaurantId,
                M03CreatedBy = dto.M03CreatedBy,
                M03CreatedAt = DateTime.UtcNow
            };

            await _voucherRepository.AddAsync(voucher);

            var voucherDto = new VoucherDto
            {
                M03Id = voucher.M03Id,
                M03Code = voucher.M03Code,
                M03Description = voucher.M03Description,
                M03DiscountValue = voucher.M03DiscountValue,
                M03IsActive = voucher.M03IsActive,
                M03MinOrderValue = voucher.M03MinOrderValue,
                M03MaxDiscount = voucher.M03MaxDiscount,
                M03FromDate = voucher.M03FromDate,
                M03ToDate = voucher.M03ToDate,
                M03UsageLimit = voucher.M03UsageLimit,
                M03RestaurantId = voucher.M03RestaurantId,
                M03CreatedAt = voucher.M03CreatedAt,
                M03CreatedBy = voucher.M03CreatedBy
            };

            return ServiceResponse<VoucherDto>.Created(voucherDto, "Voucher created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<VoucherDto>.Error($"Error creating voucher: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateVoucherDto dto)
    {
        try
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            if (voucher == null)
            {
                return ServiceResponse<object>.NotFound("Voucher not found");
            }

            voucher.M03Code = dto.M03Code ?? voucher.M03Code;
            voucher.M03Description = dto.M03Description ?? voucher.M03Description;
            voucher.M03DiscountValue = dto.M03DiscountValue ?? voucher.M03DiscountValue;
            voucher.M03IsActive = dto.M03IsActive ?? voucher.M03IsActive;
            voucher.M03MinOrderValue = dto.M03MinOrderValue ?? voucher.M03MinOrderValue;
            voucher.M03MaxDiscount = dto.M03MaxDiscount ?? voucher.M03MaxDiscount;
            voucher.M03FromDate = dto.M03FromDate ?? voucher.M03FromDate;
            voucher.M03ToDate = dto.M03ToDate ?? voucher.M03ToDate;
            voucher.M03UsageLimit = dto.M03UsageLimit ?? voucher.M03UsageLimit;
            voucher.M03RestaurantId = dto.M03RestaurantId ?? voucher.M03RestaurantId;
            voucher.M03UpdatedBy = dto.M03UpdatedBy;
            voucher.M03UpdatedAt = DateTime.UtcNow;

            await _voucherRepository.UpdateAsync(voucher);

            return ServiceResponse<object>.Success(null, "Voucher updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating voucher: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(Guid id)
    {
        try
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            if (voucher == null)
            {
                return ServiceResponse<object>.NotFound("Voucher not found");
            }

            await _voucherRepository.DeleteAsync(voucher.M03Id);

            return ServiceResponse<object>.Success(null, "Voucher deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting voucher: {ex.Message}");
        }
    }
}