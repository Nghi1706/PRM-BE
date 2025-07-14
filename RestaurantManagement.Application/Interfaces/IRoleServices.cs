using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.DTOs;

namespace RestaurantManagement.Application.Interfaces;
public interface IRoleServices
{
    Task<List<RoleDto>> GetAllAsync();
    Task<RoleDto?> GetByIdAsync(Guid id);
    Task<RoleDto> CreateAsync(CreateRoleDto role);
    Task<bool> UpdateAsync(Guid id, UpdateRoleDto role);
    Task<bool> DeleteAsync(Guid id);
}