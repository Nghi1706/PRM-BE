using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using BCrypt.Net;

namespace RestaurantManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(Guid restaurantId)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                RestaurantId = u.RestaurantId,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                RoleId = u.RoleId,
                IsActive = u.IsActive,
                Password = "",
                CreatedAt = u.CreatedAt,
                CreatedByUser = u.CreatedByUser,
                UpdatedAt = u.UpdatedAt,
                UpdatedByUser = u.UpdatedByUser
            });
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;
            return new UserDto
            {
                Id = user.Id,
                RestaurantId = user.RestaurantId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                Password = "",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                CreatedByUser = user.CreatedByUser,
                UpdatedAt = user.UpdatedAt,
                UpdatedByUser = user.UpdatedByUser
            };
        }
        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            string hashedPassword = string.IsNullOrEmpty(dto.Password) ? string.Empty : BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                RestaurantId = dto.RestaurantId,
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                RoleId = dto.RoleId,
                HashedPW = hashedPassword,
                IsActive = dto.IsActive ?? true,
                CreatedAt = DateTime.UtcNow,
                CreatedByUser = dto.CreatedByUser,
            };
            await _userRepository.AddAsync(user);
            return new UserDto
            {
                Id = user.Id,
                RestaurantId = user.RestaurantId,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = user.RoleId,
                Password = "",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                CreatedByUser = user.CreatedByUser
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            user.FullName = dto.FullName ?? user.FullName;
            user.Email = dto.Email ?? user.Email;
            user.Phone = dto.Phone ?? user.Phone;
            user.RoleId = dto.RoleId ?? user.RoleId;
            user.IsActive = dto.IsActive ?? user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedByUser = dto.UpdatedByUser ?? user.UpdatedByUser;
            user.HashedPW = user.HashedPW;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if (user == null) throw new KeyNotFoundException("User not found");
            await _userRepository.DeleteAsync(user.Id);
            return true;
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string newPassword, string oldPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new KeyNotFoundException("User not found");
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.HashedPW))
            {
                return false;
            }
            user.HashedPW = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ActivateUserAsync(Guid id, bool isActive)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            user.IsActive = isActive;
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
