using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();
    Task<RestaurantDto?> GetByIdAsync(Guid id);
    Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateRestaurantDto dto);
    Task<bool> DeleteAsync(Guid id);
}
