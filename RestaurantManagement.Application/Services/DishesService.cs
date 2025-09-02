using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class DishesService : IDishesService
{
    private readonly IDishesRepository _dishRepository;

    public DishesService(IDishesRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<ServiceResponse<IEnumerable<DishesDto>>> GetAllAsync()
    {
        try
        {
            var dishes = await _dishRepository.GetAllAsync();
            var dishDtos = dishes.Select(dish => new DishesDto
            {
                M07Id = dish.M07Id,
                M07Name = dish.M07Name,
                M07Description = dish.M07Description,
                M07Price = dish.M07Price,
                M07Image = dish.M07Image,
                M07IsActive = dish.M07IsActive,
                M07CategoryId = dish.M07CategoryId,
                M07RestaurantId = dish.M07RestaurantId,
                M07CreatedAt = dish.M07CreatedAt,
                M07CreatedBy = dish.M07CreatedBy,
                M07UpdatedAt = dish.M07UpdatedAt,
                M07UpdatedBy = dish.M07UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<DishesDto>>.Success(dishDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<DishesDto>>.Error($"Error retrieving dishes: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<DishesDto>>> GetByRestaurantIdAsync(Guid restaurantId)
    {
        try
        {
            var dishes = await _dishRepository.GetByRestaurantIdAsync(restaurantId);
            var dishDtos = dishes.Select(dish => new DishesDto
            {
                M07Id = dish.M07Id,
                M07Name = dish.M07Name,
                M07Description = dish.M07Description,
                M07Price = dish.M07Price,
                M07Image = dish.M07Image,
                M07IsActive = dish.M07IsActive,
                M07CategoryId = dish.M07CategoryId,
                M07RestaurantId = dish.M07RestaurantId,
                M07CreatedAt = dish.M07CreatedAt,
                M07CreatedBy = dish.M07CreatedBy,
                M07UpdatedAt = dish.M07UpdatedAt,
                M07UpdatedBy = dish.M07UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<DishesDto>>.Success(dishDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<DishesDto>>.Error($"Error retrieving dishes: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<DishesDto>>> GetByCategoryIdAsync(int categoryId)
    {
        try
        {
            var dishes = await _dishRepository.GetByCategoryIdAsync(categoryId);
            var dishDtos = dishes.Select(dish => new DishesDto
            {
                M07Id = dish.M07Id,
                M07Name = dish.M07Name,
                M07Description = dish.M07Description,
                M07Price = dish.M07Price,
                M07Image = dish.M07Image,
                M07IsActive = dish.M07IsActive,
                M07CategoryId = dish.M07CategoryId,
                M07RestaurantId = dish.M07RestaurantId,
                M07CreatedAt = dish.M07CreatedAt,
                M07CreatedBy = dish.M07CreatedBy,
                M07UpdatedAt = dish.M07UpdatedAt,
                M07UpdatedBy = dish.M07UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<DishesDto>>.Success(dishDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<DishesDto>>.Error($"Error retrieving dishes: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<DishesDto>> GetByIdAsync(int id)
    {
        try
        {
            var dish = await _dishRepository.GetByIdAsync(id);
            if (dish == null)
            {
                return ServiceResponse<DishesDto>.NotFound("Dish not found");
            }

            var dishDto = new DishesDto
            {
                M07Id = dish.M07Id,
                M07Name = dish.M07Name,
                M07Description = dish.M07Description,
                M07Price = dish.M07Price,
                M07Image = dish.M07Image,
                M07IsActive = dish.M07IsActive,
                M07CategoryId = dish.M07CategoryId,
                M07RestaurantId = dish.M07RestaurantId,
                M07CreatedAt = dish.M07CreatedAt,
                M07CreatedBy = dish.M07CreatedBy,
                M07UpdatedAt = dish.M07UpdatedAt,
                M07UpdatedBy = dish.M07UpdatedBy
            };

            return ServiceResponse<DishesDto>.Success(dishDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<DishesDto>.Error($"Error retrieving dish: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<DishesDto>> CreateAsync(CreateDishesDto dto)
    {
        try
        {
            var dish = new Dishes
            {
                M07Name = dto.M07Name,
                M07Description = dto.M07Description,
                M07Price = dto.M07Price,
                M07Image = dto.M07Image,
                M07IsActive = dto.M07IsActive,
                M07CategoryId = dto.M07CategoryId,
                M07RestaurantId = dto.M07RestaurantId,
                M07CreatedBy = dto.M07CreatedBy,
                M07CreatedAt = DateTime.UtcNow
            };

            await _dishRepository.AddAsync(dish);

            var dishDto = new DishesDto
            {
                M07Id = dish.M07Id,
                M07Name = dish.M07Name,
                M07Description = dish.M07Description,
                M07Price = dish.M07Price,
                M07Image = dish.M07Image,
                M07IsActive = dish.M07IsActive,
                M07CategoryId = dish.M07CategoryId,
                M07RestaurantId = dish.M07RestaurantId,
                M07CreatedAt = dish.M07CreatedAt,
                M07CreatedBy = dish.M07CreatedBy
            };

            return ServiceResponse<DishesDto>.Created(dishDto, "Dish created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<DishesDto>.Error($"Error creating dish: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateDishesDto dto)
    {
        try
        {
            var dish = await _dishRepository.GetByIdAsync(id);
            if (dish == null)
            {
                return ServiceResponse<object>.NotFound("Dish not found");
            }

            dish.M07Name = dto.M07Name ?? dish.M07Name;
            dish.M07Description = dto.M07Description ?? dish.M07Description;
            dish.M07Price = dto.M07Price ?? dish.M07Price;
            dish.M07Image = dto.M07Image ?? dish.M07Image;
            dish.M07IsActive = dto.M07IsActive ?? dish.M07IsActive;
            dish.M07CategoryId = dto.M07CategoryId ?? dish.M07CategoryId;
            dish.M07RestaurantId = dto.M07RestaurantId ?? dish.M07RestaurantId;
            dish.M07UpdatedBy = dto.M07UpdatedBy;
            dish.M07UpdatedAt = DateTime.UtcNow;

            await _dishRepository.UpdateAsync(dish);

            return ServiceResponse<object>.Success(null, "Dish updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating dish: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(int id)
    {
        try
        {
            var result = await _dishRepository.DeleteAsync(id);
            if (!result)
            {
                return ServiceResponse<object>.NotFound("Dish not found");
            }

            return ServiceResponse<object>.Success(null, "Dish deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting dish: {ex.Message}");
        }
    }
}