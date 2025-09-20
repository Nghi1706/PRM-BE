# PRM-BE - Hard Data Mode

D? ?n ?? ???c c?u h?nh ?? s? d?ng hard data thay v? database th?c t?. T?t c? c?c service ??u ho?t ??ng v?i d? li?u m?u ???c ??nh ngh?a s?n.

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

- Terminal 1 - ApiGateway:
```bash
cd src/ApiGateway
dotnet run
```

- Terminal 2 - AuthService:
```bash
cd src/AuthService
dotnet run
```

- Terminal 3 - ProductService:
```bash
cd src/Services/ProductService
dotnet run
```

- Terminal 4 - OrderService:
```bash
cd src/Services/OrderService
dotnet run
```

## ? D? li?u m?u c? s?n

### **AuthService - Users**
- **admin** / admin123 (Role: Admin)
- **user** / user123 (Role: User)  
- **manager** / manager123 (Role: Manager)

### **ProductService - Products**
1. **Laptop Gaming** - $1,299.99 (Electronics)
2. **Wireless Mouse** - $29.99 (Electronics)
3. **Mechanical Keyboard** - $89.99 (Electronics)
4. **Office Chair** - $199.99 (Furniture)
5. **Coffee Mug** - $12.99 (Accessories)

### **OrderService - Orders**
1. **ORD-20241201-ABC12345** - John Doe (Delivered)
   - Laptop Gaming + Wireless Mouse
   - Total: $1,329.98
2. **ORD-20241202-DEF67890** - Jane Smith (Shipped)
   - Mechanical Keyboard
   - Total: $89.99
3. **ORD-20241203-GHI11111** - John Doe (Pending)
   - Office Chair + Coffee Mug
   - Total: $212.98

## ? API Endpoints

### **API Gateway (Ocelot)**
- URL: http://localhost:5001
- Swagger: http://localhost:5001/swagger

### **AuthService**
- Direct URL: http://localhost:5064
- Through Gateway: http://localhost:5001/auth
- **POST /api/auth/login** - ??ng nh?p
- **POST /api/auth/logout** - ??ng xu?t

### **ProductService**
- Direct URL: http://localhost:5110
- Through Gateway: http://localhost:5001/products
- **GET /api/product** - L?y t?t c? s?n ph?m
- **GET /api/product/{id}** - L?y s?n ph?m theo ID
- **POST /api/product** - T?o s?n ph?m m?i
- **PUT /api/product/{id}** - C?p nh?t s?n ph?m
- **DELETE /api/product/{id}** - X?a s?n ph?m
- **GET /api/product/category/{category}** - L?y s?n ph?m theo danh m?c

### **OrderService**
- Direct URL: http://localhost:5014
- Through Gateway: http://localhost:5001/orders
- **GET /api/order** - L?y t?t c? ??n h?ng
- **GET /api/order/{id}** - L?y ??n h?ng theo ID
- **GET /api/order/number/{orderNumber}** - L?y ??n h?ng theo s? ??n h?ng
- **GET /api/order/customer/{customerId}** - L?y ??n h?ng theo customer
- **GET /api/order/status/{status}** - L?y ??n h?ng theo tr?ng th?i
- **POST /api/order** - T?o ??n h?ng m?i
- **PUT /api/order/{id}** - C?p nh?t ??n h?ng
- **PATCH /api/order/{id}/status** - C?p nh?t tr?ng th?i ??n h?ng
- **DELETE /api/order/{id}** - X?a ??n h?ng

## ? Test API

### **1. Test Authentication**
```bash
# Login
curl -X POST http://localhost:5001/auth/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username": "admin", "password": "admin123"}'

# Response s? c? token ?? s? d?ng cho c?c API kh?c
```

### **2. Test Products**
```bash
# Get all products
curl http://localhost:5001/products/api/product

# Get product by ID
curl http://localhost:5001/products/api/product/1

# Get products by category
curl http://localhost:5001/products/api/product/category/Electronics
```

### **3. Test Orders**
```bash
# Get all orders
curl http://localhost:5001/orders/api/order

# Get order by ID
curl http://localhost:5001/orders/api/order/1

# Get orders by customer
curl http://localhost:5001/orders/api/order/customer/1

# Get orders by status
curl http://localhost:5001/orders/api/order/status/Pending
```

## ? C?u h?nh

### **Database (T?m th?i b? v? hi?u h?a)**
T?t c? c?c service ?? ???c c?u h?nh ?? kh?ng s? d?ng database th?c t?. C?c d?ng code li?n quan ??n database ?? ???c comment out:

```csharp
// Add Database Context (temporarily disabled)
// builder.Services.AddDatabaseContext<AuthDbContext>(builder.Configuration, "AuthConnection");
```

### **Message Queue**
RabbitMQ v?n ho?t ??ng b?nh th??ng v?i c?u h?nh:
- Host: localhost
- Port: 5672
- Username: guest
- Password: guest

## ? L?u ? quan tr?ng

1. **D? li?u kh?ng persistent**: T?t c? d? li?u s? reset khi restart service
2. **Kh?ng c?n database**: Kh?ng c?n c?i ??t SQL Server hay t?o database
3. **D? li?u m?u**: C? th? ch?nh s?a d? li?u m?u trong c?c Repository classes
4. **API ho?t ??ng ??y ??**: T?t c? CRUD operations ??u ho?t ??ng v?i hard data

## ? Chuy?n v? Database Mode

Khi mu?n s? d?ng database th?c t?:

1. **Uncomment** c?c d?ng database context trong Program.cs
2. **C?i ??t** SQL Server
3. **T?o database** v? ch?y migration
4. **C?p nh?t** connection strings trong appsettings.json

## ? Troubleshooting

### **Port conflicts**
N?u g?p l?i port ?? ???c s? d?ng:
- AuthService: 5064
- ProductService: 5110  
- OrderService: 5014
- ApiGateway: 5001

### **RabbitMQ connection**
N?u RabbitMQ kh?ng ho?t ??ng, c? th? comment out c?c ph?n li?n quan ??n message queue.

### **CORS issues**
API Gateway ?? ???c c?u h?nh CORS cho localhost:3000 v? 192.168.10.72:3000
