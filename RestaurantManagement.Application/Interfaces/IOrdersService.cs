using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrdersService
{
    Task<ServiceResponse<IEnumerable<OrdersDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<OrdersDto>>> GetByTableIdAsync(int tableId);
    Task<ServiceResponse<OrdersDto>> GetByIdAsync(Guid id);
    Task<ServiceResponse<OrdersDto>> CreateAsync(CreateOrdersDto dto);
    Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateOrdersDto dto);
    Task<ServiceResponse<object>> DeleteAsync(Guid id);
}