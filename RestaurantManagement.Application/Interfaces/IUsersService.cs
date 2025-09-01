using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Interfaces;

public interface IUsersService
{
    Task<ServiceResponse<IEnumerable<UsersDto>>> GetAllAsync();
    Task<ServiceResponse<IEnumerable<UsersDto>>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<ServiceResponse<UsersDto>> GetByIdAsync(Guid id);
    Task<ServiceResponse<UsersDto>> GetByEmailAsync(string email);
    Task<ServiceResponse<UsersDto>> CreateAsync(CreateUsersDto dto);
    Task<ServiceResponse<object>> UpdateAsync(Guid id, UpdateUsersDto dto);
    Task<ServiceResponse<object>> DeleteAsync(Guid id);
}
