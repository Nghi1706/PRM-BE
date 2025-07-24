using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync(Guid restaurantId);
    Task<IEnumerable<OrderDto>> GetByRoomAsync(Guid roomId);
    Task<IEnumerable<OrderDto>> GetByTableAsync(Guid tableId);
    Task<OrderDto?> GetByIdAsync(Guid roomId);
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateOrderDto dto);
    Task<bool> DeleteAsync(Guid id);
}