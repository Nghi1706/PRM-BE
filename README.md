# PRM-BE - Microservices với Ocelot API Gateway

Dự án microservices sử dụng Ocelot API Gateway để quản lý routing và load balancing.

## 🏗️ Kiến trúc dự án

```
PRM-BE/
├── src/
│   ├── ApiGateway/          # Ocelot API Gateway (HTTP: 5001)
│   └── Services/
│       ├── OrderService/    # Quản lý đơn hàng (HTTP: 5014)
│       └── ProductService/  # Quản lý sản phẩm (HTTP: 5110)
│
│   └── AuthService/         # Xác thực/ủy quyền (HTTP: 5064)
```

Lưu ý: API Gateway đang được cấu hình lắng nghe tại `http://localhost:5001` trong `Program.cs` (UseUrls). Các service lắng nghe theo cổng HTTP như trên.

## 🚀 Cách chạy dự án

### 1. Restore packages
```bash
dotnet restore
```

### 2. Build solution
```bash
dotnet build
```

### 3. Chạy các services

- Terminal 1 - ApiGateway:
```bash
cd src/ApiGateway
dotnet run
```

- Terminal 2 - OrderService:
```bash
cd src/Services/OrderService
dotnet run
```

- Terminal 3 - ProductService:
```bash
cd src/Services/ProductService
dotnet run
```

- Terminal 4 - AuthService:
```bash
cd src/AuthService
dotnet run
```

## 🔗 Endpoints

### API Gateway (Ocelot)
- URL: http://localhost:5001
- Swagger: http://localhost:5001/swagger

### Order Service
- Direct URL: http://localhost:5014
- Through Gateway: http://localhost:5001/orders
- Swagger: http://localhost:5014/swagger

### Product Service
- Direct URL: http://localhost:5110
- Through Gateway: http://localhost:5001/products (nếu được cấu hình trong Ocelot)
- Swagger: http://localhost:5110/swagger

### Auth Service
- Direct URL: http://localhost:5064
- Through Gateway: http://localhost:5001/auth (POST)
- OpenAPI (JSON): http://localhost:5064/openapi/v1.json

## ⚙️ Cấu hình Ocelot

File `src/ApiGateway/ocelot.Local.json` định nghĩa routing:

```json
{
  "GlobalConfiguration": {
    "BaseUrl": "http://0.0.0.0:5001"
  },
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/order/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 5014 }],
      "UpstreamPathTemplate": "/orders/{everything}",
      "UpstreamHttpMethod": ["GET", "POST", "PUT", "DELETE"]
    },
    {
      "DownstreamPathTemplate": "/api/order",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 5014 }],
      "UpstreamPathTemplate": "/orders",
      "UpstreamHttpMethod": ["GET", "POST", "PUT", "DELETE"]
    },
    {
      "DownstreamPathTemplate": "/api/auth/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 5064 }],
      "UpstreamPathTemplate": "/auth/{everything}",
      "UpstreamHttpMethod": ["POST"]
    }
  ]
}
```

Lưu ý: File mẫu trên đã có sẵn trong repo (`ocelot.Local.json`). Khi chạy trong Docker, gateway sẽ dùng `ocelot.Docker.json` (được chọn qua biến môi trường `RUNNING_IN_DOCKER`).

## 📚 API Endpoints

### Orders
- `GET /orders` - Lấy danh sách đơn hàng
- `GET /orders/{id}` - Lấy đơn hàng theo ID
- `POST /orders` - Tạo đơn hàng mới
- `PUT /orders/{id}` - Cập nhật đơn hàng
- `DELETE /orders/{id}` - Xóa đơn hàng

### Products
- `GET /products` - Lấy danh sách sản phẩm
- `GET /products/{id}` - Lấy sản phẩm theo ID
- `POST /products` - Tạo sản phẩm mới
- `PUT /products/{id}` - Cập nhật sản phẩm
- `DELETE /products/{id}` - Xóa sản phẩm

### Auth
- `POST /auth/login` - Đăng nhập
- `POST /auth/logout` - Đăng xuất

## 🧰 Công nghệ sử dụng

- **.NET 9.0** - Framework chính
- **Ocelot** - API Gateway
- **ASP.NET Core Web API** - Microservices
- **Swagger/OpenAPI** - Tài liệu API

## 📦 Yêu cầu hệ thống

- .NET 9.0 SDK
- Visual Studio 2022 hoặc VS Code
- Git

## 🐞 Debug và Development

### Mở trong Visual Studio
1. Mở file `PRM-BE.sln`
2. Set multiple startup projects:
   - ApiGateway
   - OrderService
   - ProductService
   - AuthService

### Mở trong VS Code
```bash
code .
```

## 📖 Tài liệu tham khảo

- [Ocelot Documentation](https://ocelot.readthedocs.io/)
- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core/)
- [Microservices Architecture](https://microservices.io/)
