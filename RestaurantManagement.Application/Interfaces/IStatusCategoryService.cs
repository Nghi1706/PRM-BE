// using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Interfaces;

using RestaurantManagement.Application.DTOs;


public interface IStatusCategoryService
{
    Task<IEnumerable<StatusCategoryDto>> GetAllAsync();
    Task<StatusCategoryDto?> GetByIdAsync(Guid id);
    Task<StatusCategoryDto> CreateAsync(CreateStatusCategoryDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateStatusCategoryDto dto);
    Task<bool> DeleteAsync(Guid id);
}
