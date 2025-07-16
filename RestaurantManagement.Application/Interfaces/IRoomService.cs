
using RestaurantManagement.Application.DTOs;
namespace RestaurantManagement.Application.Interfaces;
public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync(Guid restaurantId);
    Task<RoomDto?> GetByIdAsync(Guid id);
    Task<RoomDto> CreateAsync(CreateRoomDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateRoomDto dto);
    Task<bool> DeleteAsync(Guid id);
}