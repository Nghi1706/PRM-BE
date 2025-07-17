using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<IEnumerable<DishDto>> GetAllAsync(Guid restaurantId)
    {
        var dishes = await _dishRepository.GetAllAsync(restaurantId);
        return dishes.Select(d => new DishDto
        {
            Id = d.Id,
            RestaurantId = d.RestaurantId,
            CategoryId = d.CategoryId,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            ImageUrl = d.ImageUrl,
            IsActive = d.IsActive,
            CreatedAt = d.CreatedAt,
            CreatedByUser = d.CreatedByUser,
            UpdatedAt = d.UpdatedAt,
            UpdatedByUser = d.UpdatedByUser
        });
    }

    public async Task<DishDto?> GetByIdAsync(Guid id)
    {
        var d = await _dishRepository.GetByIdAsync(id);
        if (d == null) return null;
        return new DishDto
        {
            Id = d.Id,
            RestaurantId = d.RestaurantId,
            CategoryId = d.CategoryId,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            ImageUrl = d.ImageUrl,
            IsActive = d.IsActive,
            CreatedAt = d.CreatedAt,
            CreatedByUser = d.CreatedByUser,
            UpdatedAt = d.UpdatedAt,
            UpdatedByUser = d.UpdatedByUser
        };
    }

    public async Task<IEnumerable<DishDto>> GetByIdCategoryAsync( Guid categoryId)
    {
        var dishes = await _dishRepository.GetByIdCategoryAsync(categoryId);
        return dishes.Select(d => new DishDto
        {
            Id = d.Id,
            RestaurantId = d.RestaurantId,
            CategoryId = d.CategoryId,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            ImageUrl = d.ImageUrl,
            IsActive = d.IsActive,
            CreatedAt = d.CreatedAt,
            CreatedByUser = d.CreatedByUser,
            UpdatedAt = d.UpdatedAt,
            UpdatedByUser = d.UpdatedByUser
        });
    }

    public async Task<DishDto> CreateAsync(CreateDishDto dto)
    {
        var entity = new Dish
        {
            RestaurantId = dto.RestaurantId,
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _dishRepository.AddAsync(entity);
        return new DishDto
        {
            Id = entity.Id,
            RestaurantId = entity.RestaurantId,
            CategoryId = entity.CategoryId,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            ImageUrl = entity.ImageUrl,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            CreatedByUser = entity.CreatedByUser
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateDishDto dto)
    {
        var entity = await _dishRepository.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Name = dto.Name ?? entity.Name;
        entity.Description = dto.Description ?? entity.Description;
        entity.Price = dto.Price ?? entity.Price;
        entity.ImageUrl = dto.ImageUrl ?? entity.ImageUrl;
        entity.IsActive = dto.IsActive ?? entity.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedByUser = dto.UpdatedByUser ?? entity.UpdatedByUser;
        await _dishRepository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _dishRepository.DeleteAsync(id);
    }
}