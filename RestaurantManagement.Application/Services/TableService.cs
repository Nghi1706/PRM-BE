using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<IEnumerable<TableDto>> GetAllAsync(Guid roomId)
    {
        var tables = await _tableRepository.GetAllAsync(roomId);
        return tables.Select(t => new TableDto
        {
            Id = t.Id,
            RoomId = t.RoomId,
            Name = t.Name,
            Position = t.Position,
            StatusId = t.StatusId,
            IsActive = t.IsActive,
            CreatedAt = t.CreatedAt,
            CreatedByUser = t.CreatedByUser,
            UpdatedAt = t.UpdatedAt,
            UpdatedByUser = t.UpdatedByUser
        });
    }

    public async Task<TableDto?> GetByIdAsync(Guid id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null) return null;
        return new TableDto
        {
            Id = table.Id,
            RoomId = table.RoomId,
            Name = table.Name,
            Position = table.Position,
            IsActive = table.IsActive,
            CreatedAt = table.CreatedAt,
            CreatedByUser = table.CreatedByUser,
            UpdatedAt = table.UpdatedAt,
            UpdatedByUser = table.UpdatedByUser
        };
    }

    public async Task<TableDto> CreateAsync(CreateTableDto dto)
    {
        var table = new Table
        {
            RoomId = dto.RoomId,
            Name = dto.Name,
            StatusId = dto.StatusId,
            Position = dto.Position,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _tableRepository.AddAsync(table);
        if (table.Id == Guid.Empty) throw new InvalidOperationException("Table creation failed, ID is empty.");
        return new TableDto
        {
            Id = table.Id,
            RoomId = table.RoomId,
            Name = table.Name,
            StatusId = table.StatusId,
            Position = table.Position,
            IsActive = table.IsActive,
            CreatedAt = table.CreatedAt,
            CreatedByUser = table.CreatedByUser
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTableDto dto)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null) return false;
        table.Name = dto.Name ?? table.Name;
        table.Position = dto.Position ?? table.Position;
        table.IsActive = dto.IsActive ?? table.IsActive;
        table.UpdatedAt = DateTime.UtcNow;
        table.UpdatedByUser = dto.UpdatedByUser;
        return await _tableRepository.UpdateAsync(table);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _tableRepository.DeleteAsync(id);
    }
}