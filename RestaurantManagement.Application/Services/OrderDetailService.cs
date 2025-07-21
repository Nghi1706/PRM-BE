using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _repo;

    public OrderDetailService(IOrderDetailRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<OrderDetailDto>> GetAllAsync(Guid orderId)
        => (await _repo.GetAllAsync(orderId)).Select(Map);

    public async Task<OrderDetailDto?> GetByIdAsync(Guid id)
        => Map(await _repo.GetByIdAsync(id));

    public async Task<OrderDetailDto> CreateAsync(CreateOrderDetailDto dto)
    {
        var entity = new OrderDetail
        {
            Id = Guid.NewGuid(),
            OrderId = dto.OrderId,
            DishId = dto.DishId,
            Quantity = dto.Quantity,
            Price = dto.Price,
            StatusId = dto.StatusId,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _repo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateOrderDetailDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Quantity = dto.Quantity ?? entity.Quantity;
        entity.Price = dto.Price ?? entity.Price;
        entity.StatusId = dto.StatusId ?? entity.StatusId;
        entity.UpdatedAt = dto.UpdatedAt ?? DateTime.UtcNow;
        entity.UpdatedByUser = dto.UpdatedByUser ?? entity.UpdatedByUser;
        await _repo.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
        => await _repo.DeleteAsync(id);

    private static OrderDetailDto Map(OrderDetail entity) => new()
    {
        Id = entity.Id,
        OrderId = entity.OrderId,
        DishId = entity.DishId,
        Quantity = entity.Quantity,
        Price = entity.Price,
        StatusId = entity.StatusId,
        CreatedAt = entity.CreatedAt,
        CreatedByUser = entity.CreatedByUser,
        UpdatedAt = entity.UpdatedAt,
        UpdatedByUser = entity.UpdatedByUser
    };
}