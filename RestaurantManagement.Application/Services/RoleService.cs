using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Application.Services;

public class RoleService : IRoleServices
{
    private readonly IRoleRepository _roleRepository;
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<List<RoleDto>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(role => new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt,
            CreatedByUser = role.CreatedByUser,
            UpdatedAt = role.UpdatedAt,
            UpdatedByUser = role.UpdatedByUser
        }).ToList();
    }
    public async Task<RoleDto?> GetByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return null;
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt,
            CreatedByUser = role.CreatedByUser,
            UpdatedAt = role.UpdatedAt,
            UpdatedByUser = role.UpdatedByUser
        };
    }
    public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
    {
        var role = new Role
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive ?? true
        };
        await _roleRepository.AddAsync(role);
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
        };
    }
    public async Task<bool> UpdateAsync(Guid id, UpdateRoleDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) throw new KeyNotFoundException("Role not found");
        role.Name = dto.Name ?? role.Name;
        role.Description = dto.Description ?? role.Description;
        role.IsActive = dto.IsActive ?? role.IsActive;
        role.UpdatedAt = DateTime.UtcNow;
        await _roleRepository.UpdateAsync(role);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return false;
        await _roleRepository.DeleteAsync(role.Id);
        return true;
    }

}






