using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface IDishService
{
    Task<IEnumerable<DishDto>> GetAllAsync(Guid restaurantId);
    Task<DishDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<DishDto>> GetByIdCategoryAsync(Guid categoryId);
    Task<DishDto> CreateAsync(CreateDishDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateDishDto dto);
    Task<bool> DeleteAsync(Guid id);
}