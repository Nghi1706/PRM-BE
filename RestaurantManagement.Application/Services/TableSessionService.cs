using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class TableSessionService : ITableSessionService
{
    private readonly ITableSessionRepository _repo;

    public TableSessionService(ITableSessionRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TableSessionDto>> GetAllAsync(Guid RoomId)
        => (await _repo.GetAllAsync(RoomId)).Select(Map);

    public async Task<TableSessionDto?> GetByIdAsync(Guid id)
        => Map(await _repo.GetByIdAsync(id));

    public async Task<TableSessionDto> CreateAsync(CreateTableSessionDto dto)
    {
        var entity = new TableSession
        {
            Id = Guid.NewGuid(),
            TableId = dto.TableId,
            OrderId = dto.OrderId,
            QRToken = dto.QRToken,
            IsActive = true,
            StartedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _repo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTableSessionDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        entity.IsActive = dto.IsActive ?? entity.IsActive;
        entity.EndedAt = dto.EndedAt ?? entity.EndedAt;
        entity.UpdatedAt = dto.UpdatedAt ?? DateTime.UtcNow;
        entity.UpdatedByUser = dto.UpdatedByUser ?? entity.UpdatedByUser;
        await _repo.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repo.DeleteAsync(id);

    private static TableSessionDto Map(TableSession entity) => new()
    {
        Id = entity.Id,
        TableId = entity.TableId,
        OrderId = entity.OrderId,
        QRToken = entity.QRToken,
        IsActive = entity.IsActive,
        StartedAt = entity.StartedAt,
        EndedAt = entity.EndedAt,
        CreatedAt = entity.CreatedAt,
        CreatedByUser = entity.CreatedByUser,
        UpdatedAt = entity.UpdatedAt,
        UpdatedByUser = entity.UpdatedByUser
    };
}