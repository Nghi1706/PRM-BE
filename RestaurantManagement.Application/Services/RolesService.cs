using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Shared.Enums;

namespace RestaurantManagement.Application.Services;

public class RolesService : IRolesServices
{
    private readonly IRolesRepository _roleRepository;
    public RolesService(IRolesRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ServiceResponse<IEnumerable<RolesDto>>> GetAllAsync()
    {
        try
        {
            // Return enum values instead of database data
            var roles = Enum.GetValues<UserRole>()
                .Select(role => new RolesDto
                {
                    M01Id = (int)role,
                    M01Name = role.ToString(),
                    M01Description = GetRoleDescription(role),
                    M01IsActive = true
                }).ToList();

            return ServiceResponse<IEnumerable<RolesDto>>.Success(roles);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<RolesDto>>.Error($"Error retrieving roles: {ex.Message}");
        }
    }

    private static string GetRoleDescription(UserRole role)
    {
        return role switch
        {
            UserRole.Develop => "development - app",
            UserRole.Admin => "admin - restaurant",
            UserRole.Manager => "manage - restaurant",
            UserRole.Employee => "employee - restaurant",
            UserRole.Chef => "chef - restaurant",
            UserRole.Guest => "guest - restaurant",
            _ => ""
        };
    }

    public async Task<ServiceResponse<RolesDto>> GetByIdAsync(int id)
    {
        try
        {
            if (Enum.IsDefined(typeof(UserRole), id))
            {
                var role = (UserRole)id;
                var roleDto = new RolesDto
                {
                    M01Id = id,
                    M01Name = role.ToString(),
                    M01Description = GetRoleDescription(role),
                    M01IsActive = true
                };

                return ServiceResponse<RolesDto>.Success(roleDto);
            }

            return ServiceResponse<RolesDto>.NotFound("Role not found");
        }
        catch (Exception ex)
        {
            return ServiceResponse<RolesDto>.Error($"Error retrieving role: {ex.Message}");
        }
    }

    // Comment out Create, Update, Delete methods
    /*
    public async Task<ServiceResponse<RolesDto>> CreateAsync(CreateRolesDto dto)
    {
        try
        {
            var role = new Roles
            {
                M01Name = dto.M01Name,
                M01Description = dto.M01Description,
                M01IsActive = dto.M01IsActive
            };

            await _roleRepository.AddAsync(role);

            var roleDto = new RolesDto
            {
                M01Id = role.M01Id,
                M01Name = role.M01Name,
                M01Description = role.M01Description,
                M01IsActive = role.M01IsActive
            };

            return ServiceResponse<RolesDto>.Created(roleDto, "Role created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<RolesDto>.Error($"Error creating role: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateRolesDto dto)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return ServiceResponse<object>.NotFound("Role not found");
            }

            role.M01Name = dto.M01Name ?? role.M01Name;
            role.M01Description = dto.M01Description ?? role.M01Description;
            role.M01IsActive = dto.M01IsActive ?? role.M01IsActive;

            await _roleRepository.UpdateAsync(role);

            return ServiceResponse<object>.Success(null, "Role updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating role: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(int id)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return ServiceResponse<object>.NotFound("Role not found");
            }

            await _roleRepository.DeleteAsync(role.M01Id);

            return ServiceResponse<object>.Success(null, "Role deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting role: {ex.Message}");
        }
    }
    */
}






