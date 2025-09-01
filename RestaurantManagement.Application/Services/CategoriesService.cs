using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categoryRepository;

    public CategoriesService(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ServiceResponse<IEnumerable<CategoriesDto>>> GetAllAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = categories.Select(category => new CategoriesDto
            {
                M06Id = category.M06Id,
                M06Name = category.M06Name,
                M06Description = category.M06Description,
                M06IsActive = category.M06IsActive,
                M06RestaurantId = category.M06RestaurantId,
                M06CreatedAt = category.M06CreatedAt,
                M06CreatedBy = category.M06CreatedBy,
                M06UpdatedAt = category.M06UpdatedAt,
                M06UpdatedBy = category.M06UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<CategoriesDto>>.Success(categoryDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<CategoriesDto>>.Error($"Error retrieving categories: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<CategoriesDto>>> GetByRestaurantIdAsync(Guid restaurantId)
    {
        try
        {
            var categories = await _categoryRepository.GetByRestaurantIdAsync(restaurantId);
            var categoryDtos = categories.Select(category => new CategoriesDto
            {
                M06Id = category.M06Id,
                M06Name = category.M06Name,
                M06Description = category.M06Description,
                M06IsActive = category.M06IsActive,
                M06RestaurantId = category.M06RestaurantId,
                M06CreatedAt = category.M06CreatedAt,
                M06CreatedBy = category.M06CreatedBy,
                M06UpdatedAt = category.M06UpdatedAt,
                M06UpdatedBy = category.M06UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<CategoriesDto>>.Success(categoryDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<CategoriesDto>>.Error($"Error retrieving categories: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<CategoriesDto>> GetByIdAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResponse<CategoriesDto>.NotFound("Categories not found");
            }

            var categoryDto = new CategoriesDto
            {
                M06Id = category.M06Id,
                M06Name = category.M06Name,
                M06Description = category.M06Description,
                M06IsActive = category.M06IsActive,
                M06RestaurantId = category.M06RestaurantId,
                M06CreatedAt = category.M06CreatedAt,
                M06CreatedBy = category.M06CreatedBy,
                M06UpdatedAt = category.M06UpdatedAt,
                M06UpdatedBy = category.M06UpdatedBy
            };

            return ServiceResponse<CategoriesDto>.Success(categoryDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<CategoriesDto>.Error($"Error retrieving category: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<CategoriesDto>> CreateAsync(CreateCategoriesDto dto)
    {
        try
        {
            var category = new Categories
            {
                M06Name = dto.M06Name,
                M06Description = dto.M06Description,
                M06IsActive = dto.M06IsActive,
                M06RestaurantId = dto.M06RestaurantId,
                M06CreatedBy = dto.M06CreatedBy,
                M06CreatedAt = DateTime.UtcNow
            };

            await _categoryRepository.AddAsync(category);

            var categoryDto = new CategoriesDto
            {
                M06Id = category.M06Id,
                M06Name = category.M06Name,
                M06Description = category.M06Description,
                M06IsActive = category.M06IsActive,
                M06RestaurantId = category.M06RestaurantId,
                M06CreatedAt = category.M06CreatedAt,
                M06CreatedBy = category.M06CreatedBy
            };

            return ServiceResponse<CategoriesDto>.Created(categoryDto, "Categories created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<CategoriesDto>.Error($"Error creating category: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateCategoriesDto dto)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResponse<object>.NotFound("Categories not found");
            }

            category.M06Name = dto.M06Name ?? category.M06Name;
            category.M06Description = dto.M06Description ?? category.M06Description;
            category.M06IsActive = dto.M06IsActive ?? category.M06IsActive;
            category.M06UpdatedBy = dto.M06UpdatedBy ?? category.M06UpdatedBy;
            category.M06UpdatedAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(category);

            return ServiceResponse<object>.Success(null, "Categories updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating category: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _categoryRepository.DeleteAsync(id);
            if (!deleted)
            {
                return ServiceResponse<object>.NotFound("Categories not found");
            }

            return ServiceResponse<object>.Success(null, "Categories deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting category: {ex.Message}");
        }
    }
}