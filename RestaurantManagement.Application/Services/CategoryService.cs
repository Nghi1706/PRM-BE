using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(Guid restaurantId)
    {
        var categories = await _categoryRepository.GetAllAsync(restaurantId);
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            RestaurantId = c.RestaurantId,
            Name = c.Name,
            Description = c.Description,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            CreatedByUser = c.CreatedByUser,
            UpdatedAt = c.UpdatedAt,
            UpdatedByUser = c.UpdatedByUser
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        var c = await _categoryRepository.GetByIdAsync(id);
        if (c == null) return null;
        return new CategoryDto
        {
            Id = c.Id,
            RestaurantId = c.RestaurantId,
            Name = c.Name,
            Description = c.Description,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            CreatedByUser = c.CreatedByUser,
            UpdatedAt = c.UpdatedAt,
            UpdatedByUser = c.UpdatedByUser
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var entity = new Category
        {
            RestaurantId = dto.RestaurantId,
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };
        await _categoryRepository.AddAsync(entity);
        return new CategoryDto
        {
            Id = entity.Id,
            RestaurantId = entity.RestaurantId,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            CreatedByUser = entity.CreatedByUser
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Name = dto.Name ?? entity.Name;
        entity.Description = dto.Description ?? entity.Description;
        entity.IsActive = dto.IsActive ?? entity.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedByUser = dto.UpdatedByUser ?? entity.UpdatedByUser;
        await _categoryRepository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }
}