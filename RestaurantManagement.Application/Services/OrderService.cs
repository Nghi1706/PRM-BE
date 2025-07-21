using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync(Guid restaurantId)
        => (await _repo.GetAllAsync(restaurantId)).Select(Map);

    public async Task<OrderDto?> GetByIdAsync(Guid id)
        => Map(await _repo.GetByIdAsync(id));

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        var entity = new Order
        {
            Id = Guid.NewGuid(),
            RestaurantId = dto.RestaurantId,
            TableId = dto.TableId,
            RoomId = dto.RoomId,
            UserId = dto.UserId,
            StatusId = dto.StatusId,
            TotalAmount = dto.TotalAmount,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _repo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateOrderDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        entity.StatusId = dto.StatusId ?? entity.StatusId;
        entity.TotalAmount = dto.TotalAmount ?? entity.TotalAmount;
        entity.UpdatedAt = dto.UpdatedAt ?? DateTime.UtcNow;
        entity.UpdatedByUser = dto.UpdatedByUser ?? entity.UpdatedByUser;
        await _repo.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repo.DeleteAsync(id);

    private static OrderDto Map(Order entity) => new()
    {
        Id = entity.Id,
        RestaurantId = entity.RestaurantId,
        TableId = entity.TableId,
        RoomId = entity.RoomId,
        UserId = entity.UserId,
        StatusId = entity.StatusId,
        TotalAmount = entity.TotalAmount,
        CreatedAt = entity.CreatedAt,
        CreatedByUser = entity.CreatedByUser,
        UpdatedAt = entity.UpdatedAt,
        UpdatedByUser = entity.UpdatedByUser
    };
}