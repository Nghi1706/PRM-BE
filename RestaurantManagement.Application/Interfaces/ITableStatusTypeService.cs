using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface ITableStatusTypeService
{
    Task<IEnumerable<TableStatusTypeDto>> GetAllAsync();
    Task<TableStatusTypeDto?> GetByIdAsync(int id);
    Task<TableStatusTypeDto> CreateAsync(CreateTableStatusTypeDto dto);
    Task<bool> UpdateAsync(int id, UpdateTableStatusTypeDto dto);
    Task<bool> DeleteAsync(int id);
}