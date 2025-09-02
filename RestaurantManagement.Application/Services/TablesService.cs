using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class TablesService : ITablesService
{
    private readonly ITablesRepository _tablesRepository;

    public TablesService(ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
    }

    public async Task<ServiceResponse<IEnumerable<TablesDto>>> GetAllAsync()
    {
        try
        {
            var tables = await _tablesRepository.GetAllAsync();
            var tableDtos = tables.Select(table => new TablesDto
            {
                M08Id = table.M08Id,
                M08Name = table.M08Name,
                M08Position = table.M08Position,
                M08IsActive = table.M08IsActive,
                M08RestaurantId = table.M08RestaurantId,
                M08StatusId = table.M08StatusId,
                M08CreatedAt = table.M08CreatedAt,
                M08CreatedBy = table.M08CreatedBy,
                M08UpdatedAt = table.M08UpdatedAt,
                M08UpdatedBy = table.M08UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<TablesDto>>.Success(tableDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<TablesDto>>.Error($"Error retrieving tables: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<IEnumerable<TablesDto>>> GetByRestaurantIdAsync(Guid restaurantId)
    {
        try
        {
            var tables = await _tablesRepository.GetByRestaurantIdAsync(restaurantId);
            var tableDtos = tables.Select(table => new TablesDto
            {
                M08Id = table.M08Id,
                M08Name = table.M08Name,
                M08Position = table.M08Position,
                M08IsActive = table.M08IsActive,
                M08RestaurantId = table.M08RestaurantId,
                M08StatusId = table.M08StatusId,
                M08CreatedAt = table.M08CreatedAt,
                M08CreatedBy = table.M08CreatedBy,
                M08UpdatedAt = table.M08UpdatedAt,
                M08UpdatedBy = table.M08UpdatedBy
            }).ToList();

            return ServiceResponse<IEnumerable<TablesDto>>.Success(tableDtos);
        }
        catch (Exception ex)
        {
            return ServiceResponse<IEnumerable<TablesDto>>.Error($"Error retrieving tables: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<TablesDto>> GetByIdAsync(int id)
    {
        try
        {
            var table = await _tablesRepository.GetByIdAsync(id);
            if (table == null)
            {
                return ServiceResponse<TablesDto>.NotFound("Table not found");
            }

            var tableDto = new TablesDto
            {
                M08Id = table.M08Id,
                M08Name = table.M08Name,
                M08Position = table.M08Position,
                M08IsActive = table.M08IsActive,
                M08RestaurantId = table.M08RestaurantId,
                M08StatusId = table.M08StatusId,
                M08CreatedAt = table.M08CreatedAt,
                M08CreatedBy = table.M08CreatedBy,
                M08UpdatedAt = table.M08UpdatedAt,
                M08UpdatedBy = table.M08UpdatedBy
            };

            return ServiceResponse<TablesDto>.Success(tableDto);
        }
        catch (Exception ex)
        {
            return ServiceResponse<TablesDto>.Error($"Error retrieving table: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<TablesDto>> CreateAsync(CreateTablesDto dto)
    {
        try
        {
            var table = new Tables
            {
                M08Name = dto.M08Name,
                M08Position = dto.M08Position,
                M08IsActive = dto.M08IsActive,
                M08RestaurantId = dto.M08RestaurantId,
                M08StatusId = dto.M08StatusId,
                M08CreatedBy = dto.M08CreatedBy,
                M08CreatedAt = DateTime.UtcNow
            };

            await _tablesRepository.AddAsync(table);

            var tableDto = new TablesDto
            {
                M08Id = table.M08Id,
                M08Name = table.M08Name,
                M08Position = table.M08Position,
                M08IsActive = table.M08IsActive,
                M08RestaurantId = table.M08RestaurantId,
                M08StatusId = table.M08StatusId,
                M08CreatedAt = table.M08CreatedAt,
                M08CreatedBy = table.M08CreatedBy
            };

            return ServiceResponse<TablesDto>.Created(tableDto, "Table created successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<TablesDto>.Error($"Error creating table: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> UpdateAsync(int id, UpdateTablesDto dto)
    {
        try
        {
            var table = await _tablesRepository.GetByIdAsync(id);
            if (table == null)
            {
                return ServiceResponse<object>.NotFound("Table not found");
            }

            table.M08Name = dto.M08Name ?? table.M08Name;
            table.M08Position = dto.M08Position ?? table.M08Position;
            table.M08IsActive = dto.M08IsActive ?? table.M08IsActive;
            table.M08RestaurantId = dto.M08RestaurantId ?? table.M08RestaurantId;
            table.M08StatusId = dto.M08StatusId ?? table.M08StatusId;
            table.M08UpdatedBy = dto.M08UpdatedBy;
            table.M08UpdatedAt = DateTime.UtcNow;

            await _tablesRepository.UpdateAsync(table);

            return ServiceResponse<object>.Success(null, "Table updated successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error updating table: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<object>> DeleteAsync(int id)
    {
        try
        {
            var deleted = await _tablesRepository.DeleteAsync(id);
            if (!deleted)
            {
                return ServiceResponse<object>.NotFound("Table not found");
            }

            return ServiceResponse<object>.Success(null, "Table deleted successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<object>.Error($"Error deleting table: {ex.Message}");
        }
    }
}