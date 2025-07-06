
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Shared.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RestaurantManagement.Application.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _repository;

    private readonly ILogger<StatusService> _logger;

    public StatusService(IStatusRepository repository, ILogger<StatusService> logger)
    {
        _logger = logger ?? AppLogger<StatusService>.Instance;

        _repository = repository;
    }

    public async Task<IEnumerable<StatusDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var json = JsonConvert.SerializeObject(entities, Formatting.Indented);
        _logger.LogInformation(json);
        return entities.Select(e => new StatusDto
        {
            Id = e.Id,
            Name = e.Name,
            Code = e.Code,
            CategoryId = e.CategoryId,
            Description = e.Description ?? string.Empty,
            IsActive = e.IsActive ?? false,
        });
    }


    public async Task<StatusDto> CreateAsync(CreateStatusDto dto)
    {
        var entity = new Status
        {
            CategoryId = dto.CategoryId,
            Name = dto.Name,
            Code = dto.Code,
            Description = dto.Description ?? string.Empty,
            IsActive = dto.IsActive ?? true,
            CreatedByUser = dto.CreatedByUser,
        };

        await _repository.AddAsync(entity);

        return new StatusDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Description = entity.Description ?? string.Empty,
            CategoryId = entity.CategoryId,
            Name = entity.Name,
            IsActive = entity.IsActive ?? true
        };
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStatusDto dto)
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
