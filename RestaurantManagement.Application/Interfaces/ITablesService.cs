using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface ITablesService
{
    Task<ServiceResponse<IEnumerable<TablesDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<TablesDto>>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<ServiceResponse<TablesDto>> GetByIdAsync(int id);
    Task<ServiceResponse<TablesDto>> CreateAsync(CreateTablesDto dto);
    Task<ServiceResponse<object>> UpdateAsync(int id, UpdateTablesDto dto);
    Task<ServiceResponse<object>> DeleteAsync(int id);
}