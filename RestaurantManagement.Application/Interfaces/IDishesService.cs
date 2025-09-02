using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface IDishesService
{
    Task<ServiceResponse<IEnumerable<DishesDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<DishesDto>>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<ServiceResponse<IEnumerable<DishesDto>>> GetByCategoryIdAsync(int categoryId);
    Task<ServiceResponse<DishesDto>> GetByIdAsync(int id);
    Task<ServiceResponse<DishesDto>> CreateAsync(CreateDishesDto dto);
    Task<ServiceResponse<object>> UpdateAsync(int id, UpdateDishesDto dto);
    Task<ServiceResponse<object>> DeleteAsync(int id);
}