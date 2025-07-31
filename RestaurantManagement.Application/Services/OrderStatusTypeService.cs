using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class OrderStatusTypeService : IOrderStatusTypeService
{
    private readonly IOrderStatusTypeRepository _repo;
    public OrderStatusTypeService(IOrderStatusTypeRepository repo) => _repo = repo;

    public async Task<IEnumerable<OrderStatusTypeDto>> GetAllAsync()
        => (await _repo.GetAllAsync()).Select(Map);

    public async Task<OrderStatusTypeDto?> GetByIdAsync(int id)
        => Map(await _repo.GetByIdAsync(id));

    public async Task<OrderStatusTypeDto> CreateAsync(CreateOrderStatusTypeDto dto)
    {
        var entity = new OrderStatusType
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };
        await _repo.AddAsync(entity);
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateOrderStatusTypeDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Name = dto.Name ?? entity.Name;
        entity.Description = dto.Description ?? entity.Description;
        entity.IsActive = dto.IsActive ?? entity.IsActive;
        await _repo.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);

    private static OrderStatusTypeDto Map(OrderStatusType entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        IsActive = entity.IsActive
    };
}