using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Application.Common;
using BCrypt.Net;

namespace RestaurantManagement.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _userRepository;

    public UsersService(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse<IEnumerable<UsersDto>>> GetAllAsync()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = users.Select(user => new UsersDto
            {
                M05Id = user.M05Id,
                M05Name = user.M05Name,
                M05Email = user.M05Email,
                M05Phone = user.M05Phone,
                M05Avatar = user.M05Avatar,
                M05RoleId = user.M05RoleId,
                M05RestaurantId = user.M05RestaurantId,
                M05IsActive = user.M05IsActive,
                M05CreatedAt = user.M05CreatedAt,
                M05CreatedBy = user.M05CreatedBy,
                M05UpdatedAt = user.M05UpdatedAt,
                M05UpdatedBy = user.M05UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<UsersDto>>.Success(userDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<UsersDto>>.Error($"Error retrieving users: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<UsersDto>>> GetByRestaurantIdAsync(Guid restaurantId)
    {
        try
        {
            var users = await _userRepository.GetByRestaurantIdAsync(restaurantId);
            var userDtos = users.Select(user => new UsersDto
            {
                M05Id = user.M05Id,
                M05Name = user.M05Name,
                M05Email = user.M05Email,
                M05Phone = user.M05Phone,
                M05Avatar = user.M05Avatar,
                M05RoleId = user.M05RoleId,
                M05RestaurantId = user.M05RestaurantId,
                M05IsActive = user.M05IsActive,
                M05CreatedAt = user.M05CreatedAt,
                M05CreatedBy = user.M05CreatedBy,
                M05UpdatedAt = user.M05UpdatedAt,
                M05UpdatedBy = user.M05UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<UsersDto>>.Success(userDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<UsersDto>>.Error($"Error retrieving users: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<UsersDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ServiceResponse<UsersDto>.NotFound("User not found");
            }

            var userDto = new UsersDto
            {
                M05Id = user.M05Id,
                M05Name = user.M05Name,
                M05Email = user.M05Email,
                M05Phone = user.M05Phone,
                M05Avatar = user.M05Avatar,
                M05RoleId = user.M05RoleId,
                M05RestaurantId = user.M05RestaurantId,
                M05IsActive = user.M05IsActive,
                M05CreatedAt = user.M05CreatedAt,
                M05CreatedBy = user.M05CreatedBy,
                M05UpdatedAt = user.M05UpdatedAt,
                M05UpdatedBy = user.M05UpdatedBy
            };

            return ServiceResponse<UsersDto>.Success(userDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<UsersDto>.Error($"Error retrieving user: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<UsersDto>> GetByEmailAsync(string email)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return ServiceResponse<UsersDto>.NotFound("User not found");
            }

            var userDto = new UsersDto
            {
                M05Id = user.M05Id,
                M05Name = user.M05Name,
                M05Email = user.M05Email,
                M05Phone = user.M05Phone,
                M05Avatar = user.M05Avatar,
                M05RoleId = user.M05RoleId,
                M05RestaurantId = user.M05RestaurantId,
                M05IsActive = user.M05IsActive,
                M05CreatedAt = user.M05CreatedAt,
                M05CreatedBy = user.M05CreatedBy,
                M05UpdatedAt = user.M05UpdatedAt,
                M05UpdatedBy = user.M05UpdatedBy
            };

            return ServiceResponse<UsersDto>.Success(userDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<UsersDto>.Error($"Error retrieving user: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<UsersDto>> CreateAsync(CreateUsersDto dto)
    {
        try
        {
            var user = new Users
            {
                M05Name = dto.M05Name,
                M05HashedPW = BCrypt.Net.BCrypt.HashPassword(dto.M05Password),
                M05Email = dto.M05Email,
                M05Phone = dto.M05Phone,
                M05Avatar = dto.M05Avatar,
                M05RoleId = dto.M05RoleId,
                M05RestaurantId = dto.M05RestaurantId,
                M05IsActive = dto.M05IsActive,
                M05CreatedBy = dto.M05CreatedBy,
                M05CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            var userDto = new UsersDto
            {
                M05Id = user.M05Id,
                M05Name = user.M05Name,
                M05Email = user.M05Email,
                M05Phone = user.M05Phone,
                M05Avatar = user.M05Avatar,
                M05RoleId = user.M05RoleId,
                M05RestaurantId = user.M05RestaurantId,
                M05IsActive = user.M05IsActive,
                M05CreatedAt = user.M05CreatedAt,
                M05CreatedBy = user.M05CreatedBy
            };

            return ServiceResponse<UsersDto>.Created(userDto, "User created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<UsersDto>.Error($"Error creating user: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateUsersDto dto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ServiceResponse<object>.NotFound("User not found");
            }

            user.M05Name = dto.M05Name ?? user.M05Name;
            user.M05Email = dto.M05Email ?? user.M05Email;
            user.M05Phone = dto.M05Phone ?? user.M05Phone;
            user.M05Avatar = dto.M05Avatar ?? user.M05Avatar;
            user.M05RoleId = dto.M05RoleId ?? user.M05RoleId;
            user.M05RestaurantId = dto.M05RestaurantId ?? user.M05RestaurantId;
            user.M05IsActive = dto.M05IsActive ?? user.M05IsActive;
            user.M05UpdatedBy = dto.M05UpdatedBy;
            user.M05UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(dto.M05Password))
            {
                user.M05HashedPW = BCrypt.Net.BCrypt.HashPassword(dto.M05Password);
            }

            await _userRepository.UpdateAsync(user);

            return ServiceResponse<object>.Success(null, "User updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating user: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(Guid id)
    {
        try
        {
            var success = await _userRepository.DeleteAsync(id);
            if (!success)
            {
                return ServiceResponse<object>.NotFound("User not found");
            }

            return ServiceResponse<object>.Success(null, "User deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting user: {ex.Message}");
        }
    }
}
