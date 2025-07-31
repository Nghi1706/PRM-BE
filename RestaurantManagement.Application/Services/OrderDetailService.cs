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

    public async Task<IEnumerable<OrderDetailDto>> CreateAsync(List<CreateOrderDetailDto> dtos)
    {
        var entities = new List<OrderDetail>();
        foreach (var dto in dtos)
        {
            var entity = new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId,
                DishId = dto.DishId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                DishStatusId = dto.DishStatusId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUser = dto.CreatedByUser
            };
            await _repo.AddAsync(entity);
            entities.Add(entity);

        }
        return entities.Select(Map);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateOrderDetailDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Quantity = dto.Quantity ?? entity.Quantity;
        entity.Price = dto.Price ?? entity.Price;
        entity.DishStatusId = dto.DishStatusId ?? entity.DishStatusId;
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
        DishName = entity.DishName,
        Quantity = entity.Quantity,
        Price = entity.Price,
        DishStatusId = entity.DishStatusId,
        DishStatusName = entity.DishStatusName,
        CreatedAt = entity.CreatedAt,
        CreatedByUser = entity.CreatedByUser,
        UpdatedAt = entity.UpdatedAt,
        UpdatedByUser = entity.UpdatedByUser
    };
}