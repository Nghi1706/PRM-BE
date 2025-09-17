# AuthService - Clean Architecture

D? ?n AuthService ???c x?y d?ng theo ki?n tr?c Clean Architecture, ??m b?o t?nh ??c l?p, d? b?o tr? v? m? r?ng.

## ?? C?u tr?c th? m?c

```
AuthService/
Ñ•ÑüÑü Domain/                     # L?p Domain (Core Business Logic)
Ñ†   Ñ•ÑüÑü Entities/              # C?c th?c th? nghi?p v?
Ñ†   Ñ†   Ñ•ÑüÑü User.cs           # Entity User
Ñ†   Ñ†   Ñ•ÑüÑü UserLogin.cs      # Entity UserLogin
Ñ†   Ñ†   Ñ§ÑüÑü LoginResponse.cs  # Entity LoginResponse
Ñ†   Ñ§ÑüÑü Interfaces/           # C?c interface c?a Domain
Ñ†       Ñ•ÑüÑü IUserRepository.cs # Interface cho User Repository
Ñ†       Ñ§ÑüÑü ITokenService.cs   # Interface cho Token Service
Ñ†
Ñ•ÑüÑü Application/               # L?p Application (Use Cases)
Ñ†   Ñ•ÑüÑü Interfaces/           # C?c interface c?a Application
Ñ†   Ñ†   Ñ§ÑüÑü IAuthService.cs   # Interface cho Auth Service
Ñ†   Ñ§ÑüÑü Services/             # C?c service c?a Application
Ñ†       Ñ§ÑüÑü AuthService.cs    # Service x? l? authentication
Ñ†
Ñ•ÑüÑü Infrastructure/           # L?p Infrastructure (External Concerns)
Ñ†   Ñ•ÑüÑü Data/                # Data Access Layer
Ñ†   Ñ†   Ñ§ÑüÑü UserRepository.cs # Implementation c?a IUserRepository
Ñ†   Ñ§ÑüÑü Services/            # External Services
Ñ†       Ñ§ÑüÑü TokenService.cs  # Implementation c?a ITokenService
Ñ†
Ñ•ÑüÑü Presentation/            # L?p Presentation (API Controllers)
Ñ†   Ñ§ÑüÑü Controllers/         # API Controllers
Ñ†       Ñ§ÑüÑü AuthController.cs # Controller x? l? authentication API
Ñ†
Ñ•ÑüÑü Program.cs               # Entry point v? DI configuration
Ñ•ÑüÑü appsettings.json         # Configuration
Ñ§ÑüÑü AuthService.csproj       # Project file
```

## ? Nguy?n t?c Clean Architecture

### 1. **Dependency Rule**
- C?c l?p b?n trong kh?ng ph? thu?c v?o c?c l?p b?n ngo?i
- Domain layer kh?ng bi?t g? v? Infrastructure hay Presentation
- Application layer ch? ph? thu?c v?o Domain layer

### 2. **Separation of Concerns**
- **Domain**: Ch?a business logic v? entities
- **Application**: Ch?a use cases v? business rules
- **Infrastructure**: Ch?a implementation c?a external concerns
- **Presentation**: Ch?a API controllers v? UI logic

### 3. **Dependency Inversion**
- High-level modules kh?ng ph? thu?c v?o low-level modules
- C? hai ??u ph? thu?c v?o abstractions (interfaces)

## ? C?c th?nh ph?n ch?nh

### Domain Layer
- **Entities**: C?c ??i t??ng nghi?p v? c?t l?i
  - `User`: Th?ng tin ng??i d?ng
  - `UserLogin`: D? li?u ??ng nh?p
  - `LoginResponse`: K?t qu? tr? v? sau ??ng nh?p

- **Interfaces**: C?c contract cho external dependencies
  - `IUserRepository`: Interface cho data access
  - `ITokenService`: Interface cho token generation

### Application Layer
- **Services**: Implement c?c use cases
  - `AuthService`: X? l? logic authentication
- **Interfaces**: ??nh ngh?a contract cho application services

### Infrastructure Layer
- **Data**: Implement data access
  - `UserRepository`: Qu?n l? d? li?u user (hi?n t?i d?ng in-memory)
- **Services**: Implement external services
  - `TokenService`: T?o v? qu?n l? JWT tokens

### Presentation Layer
- **Controllers**: API endpoints
  - `AuthController`: X? l? c?c request authentication

## ? C?u h?nh Dependency Injection

Trong `Program.cs`:

```csharp
// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, Application.Services.AuthService>();
```

## ? API Endpoints

### POST /api/auth/login
??ng nh?p ng??i d?ng

**Request:**
```json
{
  "username": "admin",
  "password": "admin123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "username": "admin",
    "role": "Admin"
  }
}
```

## ? Testing

C?u tr?c Clean Architecture gi?p d? d?ng vi?t unit tests:
- Mock c?c interfaces trong Domain layer
- Test business logic ??c l?p v?i external dependencies
- Test t?ng layer ri?ng bi?t

## ? Packages s? d?ng

- `Microsoft.AspNetCore.Authentication.JwtBearer` - JWT Authentication
- `Microsoft.AspNetCore.OpenApi` - OpenAPI/Swagger
- `System.IdentityModel.Tokens.Jwt` - JWT Token handling

## ? Lu?ng x? l? Authentication

1. **Request** Å® `AuthController.Login()`
2. **Controller** Å® `IAuthService.LoginAsync()`
3. **Application Service** Å® `IUserRepository.ValidateUserAsync()`
4. **Repository** Å® Validate user credentials
5. **Application Service** Å® `ITokenService.GenerateJwtToken()`
6. **Token Service** Å® Generate JWT token
7. **Response** Å® Return token v? user info

## ? L?i ?ch c?a Clean Architecture

1. **Testability**: D? d?ng mock v? test t?ng component
2. **Maintainability**: Code d? ??c, hi?u v? b?o tr?
3. **Flexibility**: D? d?ng thay ??i implementation m? kh?ng ?nh h??ng business logic
4. **Independence**: C?c layer ??c l?p v?i nhau
5. **Scalability**: D? d?ng m? r?ng v? th?m t?nh n?ng m?i

## ? Ph?t tri?n ti?p theo

- Th?m Entity Framework cho data persistence
- Implement logging v? monitoring
- Th?m validation v? error handling
- Implement refresh token mechanism
- Th?m unit tests v? integration tests
