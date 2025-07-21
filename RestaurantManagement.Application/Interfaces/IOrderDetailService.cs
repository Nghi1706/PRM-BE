using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderDetailService
{
    Task<IEnumerable<OrderDetailDto>> GetAllAsync(Guid orderId);
    Task<OrderDetailDto?> GetByIdAsync(Guid id);
    Task<OrderDetailDto> CreateAsync(CreateOrderDetailDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateOrderDetailDto dto);
    Task<bool> DeleteAsync(Guid id);
}