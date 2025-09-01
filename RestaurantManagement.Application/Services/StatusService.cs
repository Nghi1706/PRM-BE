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
using RestaurantManagement.Application.Common;

namespace RestaurantManagement.Application.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<ServiceResponse<IEnumerable<StatusDto>>> GetAllAsync()
    {
        try
        {
            var statuses = await _statusRepository.GetAllAsync();
            var statusDtos = statuses.Select(status => new StatusDto
            {
                M02Id = status.M02Id,
                M02Name = status.M02Name,
                M02ForTable = status.M02ForTable,
                M02IsActive = status.M02IsActive
            }).ToList();

            return ServiceResponse<IEnumerable<StatusDto>>.Success(statusDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<StatusDto>>.Error($"Error retrieving statuses: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<StatusDto>> GetByIdAsync(int id)
    {
        try
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null)
            {
                return ServiceResponse<StatusDto>.NotFound("Status not found");
            }

            var statusDto = new StatusDto
            {
                M02Id = status.M02Id,
                M02Name = status.M02Name,
                M02ForTable = status.M02ForTable,
                M02IsActive = status.M02IsActive
            };

            return ServiceResponse<StatusDto>.Success(statusDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<StatusDto>.Error($"Error retrieving status: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<StatusDto>> CreateAsync(CreateStatusDto dto)
    {
        try
        {
            var status = new Status
            {
                M02Name = dto.M02Name,
                M02ForTable = dto.M02ForTable,
                M02IsActive = dto.M02IsActive
            };

            await _statusRepository.AddAsync(status);

            var statusDto = new StatusDto
            {
                M02Id = status.M02Id,
                M02Name = status.M02Name,
                M02ForTable = status.M02ForTable,
                M02IsActive = status.M02IsActive
            };

            return ServiceResponse<StatusDto>.Created(statusDto, "Status created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<StatusDto>.Error($"Error creating status: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateStatusDto dto)
    {
        try
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null)
            {
                return ServiceResponse<object>.NotFound("Status not found");
            }

            status.M02Name = dto.M02Name ?? status.M02Name;
            status.M02ForTable = dto.M02ForTable ?? status.M02ForTable;
            status.M02IsActive = dto.M02IsActive ?? status.M02IsActive;

            await _statusRepository.UpdateAsync(status);

            return ServiceResponse<object>.Success(null, "Status updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating status: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(int id)
    {
        try
        {
            var status = await _statusRepository.GetByIdAsync(id);
            if (status == null)
            {
                return ServiceResponse<object>.NotFound("Status not found");
            }

            await _statusRepository.DeleteAsync(status.M02Id);

            return ServiceResponse<object>.Success(null, "Status deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting status: {ex.Message}");
        }
    }
}
