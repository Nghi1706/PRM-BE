using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync(Guid RestaurantId);
        Task<UserDto?> GetByIdAsync(Guid Id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<bool> UpdateAsync(Guid Id, UpdateUserDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<bool> ChangePasswordAsync(Guid Id, string newPassword, string oldPassword);
        Task<bool> ActivateUserAsync(Guid Id, bool IsActive);
    }
}
