using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Services;

public class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantsRepository _restaurantRepository;

    public RestaurantsService(IRestaurantsRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<ServiceResponse<IEnumerable<RestaurantsDto>>> GetAllAsync()
    {
        try
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            var restaurantDtos = restaurants.Select(restaurant => new RestaurantsDto
            {
                M04Id = restaurant.M04Id,
                M04Name = restaurant.M04Name,
                M04Address = restaurant.M04Address,
                M04Phone = restaurant.M04Phone,
                M04Email = restaurant.M04Email,
                M04Logo = restaurant.M04Logo,
                M04IsActive = restaurant.M04IsActive,
                M04CreatedAt = restaurant.M04CreatedAt,
                M04CreatedBy = restaurant.M04CreatedBy,
                M04UpdatedAt = restaurant.M04UpdatedAt,
                M04UpdatedBy = restaurant.M04UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<RestaurantsDto>>.Success(restaurantDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<RestaurantsDto>>.Error($"Error retrieving restaurants: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<RestaurantsDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return ServiceResponse<RestaurantsDto>.NotFound("Restaurant not found");
            }

            var restaurantDto = new RestaurantsDto
            {
                M04Id = restaurant.M04Id,
                M04Name = restaurant.M04Name,
                M04Address = restaurant.M04Address,
                M04Phone = restaurant.M04Phone,
                M04Email = restaurant.M04Email,
                M04Logo = restaurant.M04Logo,
                M04IsActive = restaurant.M04IsActive,
                M04CreatedAt = restaurant.M04CreatedAt,
                M04CreatedBy = restaurant.M04CreatedBy,
                M04UpdatedAt = restaurant.M04UpdatedAt,
                M04UpdatedBy = restaurant.M04UpdatedBy
            };

            return ServiceResponse<RestaurantsDto>.Success(restaurantDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<RestaurantsDto>.Error($"Error retrieving restaurant: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<RestaurantsDto>> CreateAsync(CreateRestaurantsDto dto)
    {
        try
        {
            var restaurant = new Restaurants
            {
                M04Name = dto.M04Name,
                M04Address = dto.M04Address,
                M04Phone = dto.M04Phone,
                M04Email = dto.M04Email,
                M04Logo = dto.M04Logo,
                M04IsActive = dto.M04IsActive,
                M04CreatedBy = dto.M04CreatedBy,
                M04CreatedAt = DateTime.UtcNow
            };

            await _restaurantRepository.AddAsync(restaurant);

            var restaurantDto = new RestaurantsDto
            {
                M04Id = restaurant.M04Id,
                M04Name = restaurant.M04Name,
                M04Address = restaurant.M04Address,
                M04Phone = restaurant.M04Phone,
                M04Email = restaurant.M04Email,
                M04Logo = restaurant.M04Logo,
                M04IsActive = restaurant.M04IsActive,
                M04CreatedAt = restaurant.M04CreatedAt,
                M04CreatedBy = restaurant.M04CreatedBy
            };

            return ServiceResponse<RestaurantsDto>.Created(restaurantDto, "Restaurant created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<RestaurantsDto>.Error($"Error creating restaurant: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateRestaurantsDto dto)
    {
        try
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return ServiceResponse<object>.NotFound("Restaurant not found");
            }

            restaurant.M04Name = dto.M04Name ?? restaurant.M04Name;
            restaurant.M04Address = dto.M04Address ?? restaurant.M04Address;
            restaurant.M04Phone = dto.M04Phone ?? restaurant.M04Phone;
            restaurant.M04Email = dto.M04Email ?? restaurant.M04Email;
            restaurant.M04Logo = dto.M04Logo ?? restaurant.M04Logo;
            restaurant.M04IsActive = dto.M04IsActive ?? restaurant.M04IsActive;
            restaurant.M04UpdatedBy = dto.M04UpdatedBy;
            restaurant.M04UpdatedAt = DateTime.UtcNow;

            await _restaurantRepository.UpdateAsync(restaurant);

            return ServiceResponse<object>.Success(null, "Restaurant updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating restaurant: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(Guid id)
    {
        try
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return ServiceResponse<object>.NotFound("Restaurant not found");
            }

            await _restaurantRepository.DeleteAsync(id);

            return ServiceResponse<object>.Success(null, "Restaurant deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting restaurant: {ex.Message}");
        }
    }
}