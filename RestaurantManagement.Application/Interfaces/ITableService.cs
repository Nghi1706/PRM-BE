using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface ITableService
{
    Task<IEnumerable<TableDto>> GetAllAsync(Guid roomId);
    Task<TableDto?> GetByIdAsync(Guid id);
    Task<TableDto> CreateAsync(CreateTableDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateTableDto dto);
    Task<bool> DeleteAsync(Guid id);
}