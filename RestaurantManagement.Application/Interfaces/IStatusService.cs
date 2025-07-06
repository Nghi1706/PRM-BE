// using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Interfaces;

using RestaurantManagement.Application.DTOs;


public interface IStatusService
{
    Task<IEnumerable<StatusDto>> GetAllAsync();
    Task<StatusDto> CreateAsync(CreateStatusDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateStatusDto dto);
    Task<bool> DeleteAsync(Guid id);
}
