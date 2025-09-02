using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderDetailsService
{
    Task<ServiceResponse<IEnumerable<OrderDetailsDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<OrderDetailsDto>>> GetByOrderIdAsync(Guid orderId);
    Task<ServiceResponse<OrderDetailsDto>> GetByIdAsync(Guid id);
    Task<ServiceResponse<OrderDetailsDto>> CreateAsync(CreateOrderDetailsDto dto);
    Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateOrderDetailsDto dto);
    Task<ServiceResponse<object>> DeleteAsync(Guid id);
}