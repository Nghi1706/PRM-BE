# Restaurant Management System - AI Agent Instructions

## Architecture Overview

This is a .NET 8 restaurant management API following Clean Architecture principles with 5 layers:

- **Api**: Minimal API endpoints using RouteGroupBuilder pattern
- **Application**: Business logic, services, DTOs, and mapping
- **Domain**: Entity models with database column attributes
- **Infrastructure**: EF Core repositories and data access
- **Shared**: Common responses and utilities

## Key Patterns & Conventions

### Entity Naming Convention

All entities use Vietnamese database naming (M##\_field format):

```csharp
[Column("m09_id")]
public Guid M09Id { get; set; }
```

When adding new entities, follow this pattern and map to descriptive C# property names.

### Endpoint Structure

All endpoints use RouteGroupBuilder with centralized mapping in `MapEndpoints.cs`:

```csharp
group.MapGroup("/tables").MapTablesEndpoints();
```

New endpoints should follow this pattern and be added to `MapAppEndpoints()`.

### Service Response Pattern

All services return `ServiceResponse<T>` with consistent success/error handling:

```csharp
public static ServiceResponse<T> Success(T data, string message = "Operation completed successfully")
public static ServiceResponse<T> NotFound(string message = "Resource not found")
```

Use `ToApiResult()` extension to convert to HTTP responses in endpoints.

### Dependency Injection Registration

Services are registered in layer-specific DI files:

- `Application/DependencyInjection.cs` - Business services
- `Infrastructure/DependencyInjection.cs` - Repositories and DbContext
- Both v1 and v2 service registrations exist (v2 is current)

### Authentication & Security

- JWT Bearer authentication with custom settings in `JwtSettings`
- Environment variables loaded via DotNetEnv
- CORS configured for development (AllowAnyOrigin)
- Swagger with JWT Bearer support configured

## Development Workflows

### Database Connection

Uses SQL Server with connection string in `appsettings.json`. Entity Framework migrations should be run from Infrastructure project:

```bash
dotnet ef migrations add MigrationName --project RestaurantManagement.Infrastructure
dotnet ef database update --project RestaurantManagement.Infrastructure
```

### Running the Application

The API runs on `http://0.0.0.0:5000` and `https://0.0.0.0:5001` with Swagger at `/swagger`.

### Adding New Features

1. Create entity in `Domain/Entities` with proper column attributes
2. Add to `AppDbContext` DbSet
3. Create DTOs in `Application/DTOs` (Dto, CreateDto, UpdateDto pattern)
4. Create interface and service in `Application/Services`
5. Create repository interface and implementation in `Infrastructure`
6. Register services in respective DI files
7. Create endpoint file in `Api/Endpoints` and register in `MapEndpoints.cs`
8. Add AutoMapper profile in `Application/Mapping` if needed

## Critical Integration Points

### Database Layer

- `AppDbContext` contains both v1 (commented) and v2 (active) DbSets
- Entity configurations use `ToTable()` method in `OnModelCreating`
- Repository pattern with generic interfaces in Domain layer

### AutoMapper Usage

Mapping profiles follow naming pattern: `{Entity}Profile.cs`
Standard mappings: Entity ↔ Dto, CreateDto → Entity, UpdateDto → Entity

### Authentication Flow

- Login endpoint returns JWT + refresh token
- RefreshToken endpoint for token renewal
- Logout endpoint invalidates refresh tokens
- All secured endpoints require "Bearer {token}" header

## Project-Specific Notes

- Mixed v1/v2 code exists - v2 is the current active version
- Vietnamese comments in some files indicate legacy/configuration notes
- HTTPS redirect is intentionally disabled for development
- Entity naming uses Hungarian notation (M##) mapping to legacy database schema
- All entities use Guid primary keys except for some legacy integer keys
