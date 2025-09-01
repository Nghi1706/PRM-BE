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
            var roles = await _roleRepository.GetAllAsync();
            var roleDtos = roles.Select(role => new RolesDto
            {
                M01Id = role.M01Id,
                M01Name = role.M01Name,
                M01Description = role.M01Description,
                M01IsActive = role.M01IsActive,
            }).ToList();

            return ServiceResponse<IEnumerable<RolesDto>>.Success(roleDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<RolesDto>>.Error($"Error retrieving roles: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<RolesDto>> GetByIdAsync(int id)
    {
        try
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return ServiceResponse<RolesDto>.NotFound("Role not found");
            }

            var roleDto = new RolesDto
            {
                M01Id = role.M01Id,
                M01Name = role.M01Name,
                M01Description = role.M01Description,
                M01IsActive = role.M01IsActive
            };

            return ServiceResponse<RolesDto>.Success(roleDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<RolesDto>.Error($"Error retrieving role: {ex.Message}");
        }
    }

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
}






