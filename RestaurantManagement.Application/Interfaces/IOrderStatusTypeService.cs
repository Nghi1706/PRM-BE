using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface IOrderStatusTypeService
{
    Task<IEnumerable<OrderStatusTypeDto>> GetAllAsync();
    Task<OrderStatusTypeDto?> GetByIdAsync(int id);
    Task<OrderStatusTypeDto> CreateAsync(CreateOrderStatusTypeDto dto);
    Task<bool> UpdateAsync(int id, UpdateOrderStatusTypeDto dto);
    Task<bool> DeleteAsync(int id);
}