
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
namespace RestaurantManagement.Application.Services;

public class StatusCategoryService : IStatusCategoryService
{
    private readonly IStatusCategoryRepository _repository;

    public StatusCategoryService(IStatusCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StatusCategoryDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new StatusCategoryDto
        {
            Id = e.Id,
            Name = e.Name,
            IsActive = e.IsActive ?? false,
        });
    }

    public async Task<StatusCategoryDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;

        return new StatusCategoryDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public async Task<StatusCategoryDto> CreateAsync(CreateStatusCategoryDto dto)
    {
        var entity = new StatusCategory
        {
            Name = dto.Name,
            CreatedByUser = dto.CreatedByUser,
        };

        await _repository.AddAsync(entity);

        return new StatusCategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive ?? false
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStatusCategoryDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.Update(
            name: dto.Name,
            description: dto.Description,
            isActive: dto.IsActive,
            updatedBy: dto.UpdatedByUser
        );

        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
