# Message Queue Test Guide

H??ng d?n test lu?ng message t? OrderService sang ProductService khi g?i API get orders/1.

## ? Lu?ng ho?t ??ng

1. **Client g?i API**: `GET /api/order/1`
2. **OrderService** x? l? request v? l?y th?ng tin order
3. **OrderService** g?i message `OrderViewedMessage` qua RabbitMQ
4. **ProductService** nh?n message v? x? l?
5. **ProductService** log th?ng tin s?n ph?m ???c xem

## ? C?ch test

### **1. Ch?y c?c services**

```bash
# Terminal 1 - RabbitMQ (n?u ch?a c?)
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

# Terminal 2 - OrderService
cd src/Services/OrderService
dotnet run

# Terminal 3 - ProductService  
cd src/Services/ProductService
dotnet run

# Terminal 4 - ApiGateway (optional)
cd src/ApiGateway
dotnet run
```

### **2. Test API**

```bash
# G?i API get order by ID
curl -X GET http://localhost:5014/api/order/1

# Ho?c qua API Gateway
curl -X GET http://localhost:5001/orders/api/order/1
```

### **3. Ki?m tra logs**

**OrderService logs:**
```
[OrderService] Order viewed message sent for order 1
```

**ProductService logs:**
```
[ProductService] Processing OrderViewed event: OrderId=1, OrderNumber=ORD-20241201-ABC12345, CustomerId=1, ProductIds=1,2
[ProductService] Order viewed products: Laptop Gaming, Wireless Mouse
```

## ? D? li?u test

### **Order ID 1:**
- OrderNumber: ORD-20241201-ABC12345
- Customer: John Doe (ID: 1)
- Products: Laptop Gaming (ID: 1), Wireless Mouse (ID: 2)
- Status: Delivered

### **Order ID 2:**
- OrderNumber: ORD-20241202-DEF67890  
- Customer: Jane Smith (ID: 2)
- Products: Mechanical Keyboard (ID: 3)
- Status: Shipped

### **Order ID 3:**
- OrderNumber: ORD-20241203-GHI11111
- Customer: John Doe (ID: 1)
- Products: Office Chair (ID: 4), Coffee Mug (ID: 5)
- Status: Pending

## ? C?u h?nh RabbitMQ

### **Exchange v? Queue:**
- **Exchange**: `order-exchange`
- **Routing Key**: `order.viewed`
- **Queue**: `product-order-queue`

### **Message Format:**
```json
{
  "OrderId": 1,
  "OrderNumber": "ORD-20241201-ABC12345",
  "CustomerId": 1,
  "CustomerName": "John Doe",
  "TotalAmount": 1329.98,
  "Status": "Delivered",
  "OrderDate": "2024-12-01T10:00:00Z",
  "ProductIds": [1, 2],
  "ViewedAt": "2024-12-15T15:30:00Z"
}
```

## ? Troubleshooting

### **1. RabbitMQ connection failed**
```bash
# Ki?m tra RabbitMQ status
docker ps | grep rabbitmq

# Restart RabbitMQ
docker restart rabbitmq
```

### **2. Message kh?ng ???c g?i**
- Ki?m tra logs OrderService c? l?i g? kh?ng
- Ki?m tra RabbitMQ connection string trong appsettings.json
- Ki?m tra exchange v? routing key c? ??ng kh?ng

### **3. Message kh?ng ???c nh?n**
- Ki?m tra logs ProductService
- Ki?m tra queue name c? ??ng kh?ng
- Ki?m tra message format c? ??ng kh?ng

### **4. Test v?i Postman/Insomnia**

**Request:**
```
GET http://localhost:5014/api/order/1
Content-Type: application/json
```

**Expected Response:**
```json
{
  "id": 1,
  "orderNumber": "ORD-20241201-ABC12345",
  "customerId": 1,
  "customerName": "John Doe",
  "customerEmail": "john@example.com",
  "totalAmount": 1329.98,
  "status": "Delivered",
  "orderDate": "2024-12-01T10:00:00Z",
  "shippedDate": "2024-12-03T10:00:00Z",
  "deliveredDate": "2024-12-06T10:00:00Z",
  "shippingAddress": "123 Main St, City, State 12345",
  "notes": "Please deliver during business hours",
  "createdAt": "2024-12-01T10:00:00Z",
  "updatedAt": null,
  "isDeleted": false,
  "orderItems": [
    {
      "id": 1,
      "orderId": 1,
      "productId": 1,
      "productName": "Laptop Gaming",
      "unitPrice": 1299.99,
      "quantity": 1,
      "totalPrice": 1299.99
    },
    {
      "id": 2,
      "orderId": 1,
      "productId": 2,
      "productName": "Wireless Mouse",
      "unitPrice": 29.99,
      "quantity": 1,
      "totalPrice": 29.99
    }
  ]
}
```

## ? Monitoring

### **RabbitMQ Management UI:**
- URL: http://localhost:15672
- Username: guest
- Password: guest

### **Ki?m tra Queue:**
1. V?o Queues tab
2. T?m queue `product-order-queue`
3. Ki?m tra message count v? rate

### **Ki?m tra Exchange:**
1. V?o Exchanges tab  
2. T?m exchange `order-exchange`
3. Ki?m tra message rate
