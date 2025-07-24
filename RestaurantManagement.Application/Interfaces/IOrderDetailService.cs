using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IOrderDetailService
{
    Task<IEnumerable<OrderDetailDto>> GetAllAsync(Guid orderId);
    Task<OrderDetailDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderDetailDto>> CreateAsync(List<CreateOrderDetailDto> dtos);
    //Task<OrderDetailDto> CreateListAsync(List<CreateOrderDetailDto> dtos);
    Task<bool> UpdateAsync(Guid id, UpdateOrderDetailDto dto);
    Task<bool> DeleteAsync(Guid id);
}