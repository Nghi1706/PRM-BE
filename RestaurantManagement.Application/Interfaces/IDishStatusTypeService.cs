using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface IDishStatusTypeService
{
    Task<IEnumerable<DishStatusTypeDto>> GetAllAsync();
    Task<DishStatusTypeDto?> GetByIdAsync(int id);
    Task<DishStatusTypeDto> CreateAsync(CreateDishStatusTypeDto dto);
    Task<bool> UpdateAsync(int id, UpdateDishStatusTypeDto dto);
    Task<bool> DeleteAsync(int id);
}