using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync(Guid restaurantId);
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateOrderDto dto);
    Task<bool> DeleteAsync(Guid id);
}