using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IRestaurantsService
{
    Task<ServiceResponse<IEnumerable<RestaurantsDto>>> GetAllAsync();
    Task<ServiceResponse<RestaurantsDto>> GetByIdAsync(Guid id);
    Task<ServiceResponse<RestaurantsDto>> CreateAsync(CreateRestaurantsDto dto);
    Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateRestaurantsDto dto);
    Task<ServiceResponse<object>> DeleteAsync(Guid id);
}
