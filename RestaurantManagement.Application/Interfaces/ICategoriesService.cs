using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface ICategoriesService
{
    Task<ServiceResponse<IEnumerable<CategoriesDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<CategoriesDto>>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<ServiceResponse<CategoriesDto>> GetByIdAsync(int id);
    Task<ServiceResponse<CategoriesDto>> CreateAsync(CreateCategoriesDto dto);
    Task<ServiceResponse<object>> UpdateAsync(int id, UpdateCategoriesDto dto);
    Task<ServiceResponse<object>> DeleteAsync(int id);
}