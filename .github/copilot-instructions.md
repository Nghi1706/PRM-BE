# Restaurant Management System - AI Agent Instructions

## Architecture Overview

This is a .NET 8 restaurant management API following Clean Architecture principles with 5 layers:

- **Api**: Minimal API endpoints using RouteGroupBuilder pattern
- **Application**: Business logic, services, DTOs, and interfaces
- **Domain**: Entity models with database column attributes
- **Infrastructure**: EF Core repositories and data access
- **Shared**: Common responses, enums, and utilities

## Key Patterns & Conventions

### Entity Naming Convention

All entities use Vietnamese database naming (M##\_field format) with English table names:

```csharp
[Table("dishes")]
public class Dishes
{
    [Column("m07_id")]
    [Key]
    public int M07Id { get; set; }

    [Column("m07_name")]
    public required string M07Name { get; set; }
}
```

Mixed ID types: `Guid` for most entities, `int` for categories/dishes/tables/status.

### Dual Response Pattern

Services use `ServiceResponse<T>` internally, converted to `ApiResponse<T>` at endpoints:

```csharp
// In Service
return ServiceResponse<DishesDto>.NotFound("Dish not found");

// In Endpoint
var serviceResponse = await service.GetByIdAsync(id);
return serviceResponse.ToApiResult(); // Extension converts to IResult
```

Both have same static methods: `Success()`, `NotFound()`, `Error()`, `BadRequest()`, etc.

### Endpoint Structure

All endpoints use RouteGroupBuilder with centralized mapping:

```csharp
// MapEndpoints.cs
group.MapGroup("/dishes").MapDishEndpoints();

// Program.cs
app.MapGroup("/api").MapAppEndpoints();
```

Endpoints use `[FromServices]` injection and return `ToApiResult()`.

### Version Coexistence (Critical)

**v1 (Legacy)**: Commented out in DI files but entities still exist
**v2 (Active)**: Current implementation with M## column naming

Both versions coexist in `AppDbContext` - only register v2 services.

### Authentication Flow

JWT-based auth with refresh tokens using `AuthService`:

```csharp
// JWT claims include: NameIdentifier, Email, RestaurantId, Role
// Access token: 12 hours, Refresh token: 7 days
// No database storage - tokens are stateless JWTs
```

Login returns `AuthResponseDto` with user info + both tokens.

## Development Workflows

### Database Migrations

Always run from Infrastructure project:

```bash
dotnet ef migrations add MigrationName --project RestaurantManagement.Infrastructure --startup-project RestaurantManagement.Api
dotnet ef database update --project RestaurantManagement.Infrastructure --startup-project RestaurantManagement.Api
```

### Environment Setup

- Uses `DotNetEnv` for `.env` file loading
- JWT settings configured in `Program.cs` from config/env vars
- CORS allows all origins for development
- HTTPS redirect disabled intentionally

### Building & Running

```bash
dotnet build RestaurantManagement.sln
dotnet run --project RestaurantManagement.Api
```

API runs on ports 5000 (HTTP) / 5001 (HTTPS) with Swagger at `/swagger`.

### Adding New Features

1. **Entity**: Create in `Domain/Entities` with `[Table("name")]` and `[Column("m##_field")]`
2. **DbContext**: Add `DbSet<Entity>` to v2 section + `ToTable()` in `OnModelCreating`
3. **Repository**: Interface in `Domain/Interfaces`, implementation in `Infrastructure/Repositories`
4. **DTOs**: Create `EntityDto`, `CreateEntityDto`, `UpdateEntityDto` in `Application/DTOs`
5. **Service**: Interface and implementation in `Application/Interfaces` and `Services`
6. **DI Registration**: Add to both `Application/DependencyInjection.cs` and `Infrastructure/DependencyInjection.cs`
7. **Endpoints**: Create endpoint class in `Api/Endpoints` and register in `MapEndpoints.cs`
8. **Mapping**: Manual mapping in services (no AutoMapper for v2)

## Critical Integration Points

### Service Layer Patterns

Services use constructor injection and return `ServiceResponse<T>`:

```csharp
public async Task<ServiceResponse<EntityDto>> GetByIdAsync(int id)
{
    try
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return ServiceResponse<EntityDto>.NotFound("Entity not found");

        // Manual mapping to DTO
        var dto = new EntityDto { /* map properties */ };

        return ServiceResponse<EntityDto>.Success(dto);
    }
    catch (Exception ex)
    {
        return ServiceResponse<EntityDto>.Error($"Error: {ex.Message}");
    }
}
```

### Repository Pattern

Generic repository interfaces in Domain, implementations in Infrastructure using EF Core:

```csharp
public interface IEntityRepository
{
    Task<IEnumerable<Entity>> GetAllAsync();
    Task<Entity?> GetByIdAsync(int id);
    Task AddAsync(Entity entity);
    Task UpdateAsync(Entity entity);
    Task<bool> DeleteAsync(int id);
}
```

### Password Handling

Uses `BCrypt.Net` for hashing in `UsersService.CreateAsync()` and verification in `AuthService.LoginAsync()`.

## Project-Specific Notes

- Vietnamese comments indicate development/deployment configurations
- Entity naming follows legacy database schema (M01=roles, M02=status, etc.)
- Some entities have mixed casing in table names (`Categories` vs `categories`)
- JWT tokens are stateless - no database refresh token storage in v2
- All timestamps use `DateTime.UtcNow` defaults
- Boolean fields default to `true` for active status
