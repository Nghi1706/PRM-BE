using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Services;


public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();

        return restaurants.Select(r => new RestaurantDto
        {
            Id = r.Id,
            Name = r.Name,
            Phone = r.Phone,
            Email = r.Email,
            Address = r.Address,
            IsActive = r.IsActive,
            CreatedAt = r.CreatedAt,
            CreatedByUser = r.CreatedByUser,
            UpdatedAt = r.UpdatedAt,
            UpdatedByUser = r.UpdatedByUser
        });
    }

    public async Task<RestaurantDto?> GetByIdAsync(Guid id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null) return null;
        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Phone = restaurant.Phone,
            Email = restaurant.Email,
            Address = restaurant.Address,
            IsActive = restaurant.IsActive,
            CreatedAt = restaurant.CreatedAt,
            CreatedByUser = restaurant.CreatedByUser,
            UpdatedAt = restaurant.UpdatedAt,
            UpdatedByUser = restaurant.UpdatedByUser
        };
    }

    public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto)
    {
        var restaurant = new Restaurant
        {
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email,
            IsActive = dto.IsActive ?? true,
            CreatedAt = DateTime.UtcNow,
            CreatedByUser = dto.CreatedByUser
        };

        await _restaurantRepository.AddAsync(restaurant);
        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Phone = restaurant.Phone,
            Email = restaurant.Email,
            Address = restaurant.Address,
            IsActive = restaurant.IsActive,
            CreatedAt = restaurant.CreatedAt,
            CreatedByUser = restaurant.CreatedByUser,
            UpdatedAt = restaurant.UpdatedAt,
            UpdatedByUser = restaurant.UpdatedByUser
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateRestaurantDto dto)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null) return false;

        restaurant.Name = dto.Name ?? restaurant.Name;
        restaurant.Address = dto.Address ?? restaurant.Address;
        restaurant.Phone = dto.Phone ?? restaurant.Phone;
        restaurant.Email = dto.Email ?? restaurant.Email;
        restaurant.IsActive = dto.IsActive ?? restaurant.IsActive;
        restaurant.UpdatedAt = DateTime.UtcNow;
        restaurant.UpdatedByUser = dto.UpdatedByUser;

        await _restaurantRepository.UpdateAsync(restaurant);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null) return false;
        await _restaurantRepository.DeleteAsync(restaurant.Id);
        return true;

    }
}