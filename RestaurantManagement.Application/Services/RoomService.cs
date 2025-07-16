using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
namespace RestaurantManagement.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync(Guid restaurantId)
    {
        var rooms = await _roomRepository.GetAllAsync(restaurantId);
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            RestaurantId = r.RestaurantId,
            Name = r.Name,
            Description = r.Description,
            IsActive = r.IsActive,
            CreatedAt = r.CreatedAt,
            CreatedByUser = r.CreatedByUser,
            UpdatedAt = r.UpdatedAt,
            UpdatedByUser = r.UpdatedByUser
        });
    }

    public async Task<RoomDto?> GetByIdAsync(Guid id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) return null;
        return new RoomDto
        {
            Id = room.Id,
            RestaurantId = room.RestaurantId,
            Name = room.Name,
            Description = room.Description,
            IsActive = room.IsActive,
            CreatedAt = room.CreatedAt,
            CreatedByUser = room.CreatedByUser,
            UpdatedAt = room.UpdatedAt,
            UpdatedByUser = room.UpdatedByUser
        };
    }

    public async Task<RoomDto> CreateAsync(CreateRoomDto dto)
    {
        var room = new Room
        {
            RestaurantId = dto.RestaurantId,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _roomRepository.AddAsync(room);
        var createdRoom = await _roomRepository.GetByIdAsync(room.Id);
        if (createdRoom == null)
        {
            throw new Exception("Room creation failed, room not found after creation.");
        }
        return new RoomDto
        {
            Id = createdRoom.Id,
            RestaurantId = room.RestaurantId,
            Name = room.Name,
            Description = room.Description,
            IsActive = room.IsActive,
            CreatedAt = room.CreatedAt,
            CreatedByUser = room.CreatedByUser
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateRoomDto dto)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) return false;
        room.Name = dto.Name ?? room.Name;
        room.Description = dto.Description ?? room.Description;
        room.IsActive = dto.IsActive ?? room.IsActive;
        room.UpdatedAt = DateTime.UtcNow;
        room.UpdatedByUser = dto.UpdatedByUser;
        await _roomRepository.UpdateAsync(room);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _roomRepository.DeleteAsync(id);
    }
}