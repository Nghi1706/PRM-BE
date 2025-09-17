# PRM-BE - Microservices v?i Ocelot API Gateway

D? ?n microservices s? d?ng Ocelot API Gateway ?? qu?n l? routing v? load balancing.

## ?? Ki?n tr?c d? ?n

```
PRM-BE/
„¥„Ÿ„Ÿ src/
„    „¥„Ÿ„Ÿ ApiGateway/          # Ocelot API Gateway (Port 7162/5193)
„    „¤„Ÿ„Ÿ Services/
„        „¥„Ÿ„Ÿ OrderService/    # Qu?n l? ??n h?ng (Port 7237/5014)
„        „¥„Ÿ„Ÿ UserService/     # Qu?n l? ng??i d?ng (Port 7021/5108)
„        „¤„Ÿ„Ÿ ProductService/  # Qu?n l? s?n ph?m (Port 7240/5110)
```

## ? C?ch ch?y d? ?n

### 1. Restore packages
```bash
dotnet restore
```

### 2. Build solution
```bash
dotnet build
```

### 3. Ch?y c?c services

#### Terminal 1 - ApiGateway:
```bash
cd src/ApiGateway
dotnet run
```

#### Terminal 2 - OrderService:
```bash
cd src/Services/OrderService
dotnet run
```

#### Terminal 3 - UserService:
```bash
cd src/Services/UserService
dotnet run
```

#### Terminal 4 - ProductService:
```bash
cd src/Services/ProductService
dotnet run
```

## ? Endpoints

### API Gateway (Ocelot)
- **URL**: https://localhost:7162
- **Swagger**: https://localhost:7162/swagger

### Order Service
- **Direct URL**: http://localhost:5014
- **Through Gateway**: https://localhost:7162/orders
- **Swagger**: http://localhost:5014/swagger

### User Service
- **Direct URL**: http://localhost:5108
- **Through Gateway**: https://localhost:7162/users
- **Swagger**: http://localhost:5108/swagger

### Product Service
- **Direct URL**: http://localhost:5110
- **Through Gateway**: https://localhost:7162/products
- **Swagger**: http://localhost:5110/swagger

## ? C?u h?nh Ocelot

File `src/ApiGateway/ocelot.json` ??nh ngh?a routing:

```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/orders/{everything}",
      "UpstreamPathTemplate": "/orders/{everything}",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5014
        }
      ]
    }
  ]
}
```

## ? API Endpoints

### Orders
- `GET /orders` - L?y danh s?ch ??n h?ng
- `GET /orders/{id}` - L?y ??n h?ng theo ID
- `POST /orders` - T?o ??n h?ng m?i
- `PUT /orders/{id}` - C?p nh?t ??n h?ng
- `DELETE /orders/{id}` - X?a ??n h?ng

### Users
- `GET /users` - L?y danh s?ch ng??i d?ng
- `GET /users/{id}` - L?y ng??i d?ng theo ID
- `POST /users` - T?o ng??i d?ng m?i
- `PUT /users/{id}` - C?p nh?t ng??i d?ng
- `DELETE /users/{id}` - X?a ng??i d?ng

### Products
- `GET /products` - L?y danh s?ch s?n ph?m
- `GET /products/{id}` - L?y s?n ph?m theo ID
- `POST /products` - T?o s?n ph?m m?i
- `PUT /products/{id}` - C?p nh?t s?n ph?m
- `DELETE /products/{id}` - X?a s?n ph?m

## ?? C?ng ngh? s? d?ng

- **.NET 9.0** - Framework ch?nh
- **Ocelot** - API Gateway
- **ASP.NET Core Web API** - Microservices
- **Swagger/OpenAPI** - API Documentation

## ? Y?u c?u h? th?ng

- .NET 9.0 SDK
- Visual Studio 2022 ho?c VS Code
- Git

## ? Debug v? Development

### M? trong Visual Studio
1. M? file `PRM-BE.sln`
2. Set multiple startup projects:
   - ApiGateway
   - OrderService
   - UserService
   - ProductService

### M? trong VS Code
```bash
code .
```

## ? T?i li?u tham kh?o

- [Ocelot Documentation](https://ocelot.readthedocs.io/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Microservices Architecture](https://microservices.io/)
