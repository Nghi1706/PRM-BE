using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(Guid restaurantId);
    Task<CategoryDto?> GetByIdAsync(Guid id);
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task<bool> DeleteAsync(Guid id);
}