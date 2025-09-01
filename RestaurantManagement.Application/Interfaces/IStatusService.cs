// using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Interfaces;

public interface IStatusService
{
    Task<ServiceResponse<IEnumerable<StatusDto>>> GetAllAsync();
    Task<ServiceResponse<StatusDto>> GetByIdAsync(int id);
    Task<ServiceResponse<StatusDto>> CreateAsync(CreateStatusDto dto);
    Task<ServiceResponse<object>> UpdateAsync(int id, UpdateStatusDto dto);
    Task<ServiceResponse<object>> DeleteAsync(int id);
}
