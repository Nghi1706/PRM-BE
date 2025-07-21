using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;

public interface ITableSessionService
{
    Task<IEnumerable<TableSessionDto>> GetAllAsync(Guid RoomId);
    Task<TableSessionDto?> GetByIdAsync(Guid id);
    Task<TableSessionDto> CreateAsync(CreateTableSessionDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateTableSessionDto dto);
    Task<bool> DeleteAsync(Guid id);
}