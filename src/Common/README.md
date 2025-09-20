# Common Library - Clean Architecture

Thư viện chung được xây dựng theo Clean Architecture pattern, cung cấp các services cơ bản cho Database và Message Queue.

## 🏗️ Cấu trúc thư mục

```
Common/
├── Domain/                     # Domain Layer (Core Business Logic)
│   ├── Entities/              # Base entities
│   │   └── BaseEntity.cs      # Base entity với audit fields
│   └── Interfaces/            # Domain interfaces
│       ├── IMessageQueueService.cs
│       ├── IDatabaseContext.cs
│       └── IRepository.cs
├── Application/               # Application Layer (Use Cases)
│   ├── Interfaces/            # Application interfaces
│   │   ├── IMessageQueueApplication.cs
│   │   └── IDatabaseApplication.cs
│   └── Services/              # Application services
│       ├── MessageQueueApplication.cs
│       └── DatabaseApplication.cs
├── Infrastructure/            # Infrastructure Layer (External Concerns)
│   ├── Database/              # Database implementations
│   │   ├── BaseDbContext.cs
│   │   ├── DatabaseContextFactory.cs
│   │   └── DatabaseExtensions.cs
│   ├── MessageQueue/          # Message Queue implementations
│   │   ├── RabbitMQ/
│   │   │   └── RabbitMQService.cs
│   │   └── Kafka/
│   │       └── KafkaService.cs
│   └── Repositories/          # Repository implementations
│       └── BaseRepository.cs
├── Configurations/            # Configuration classes
│   ├── DatabaseSettings.cs
│   ├── RabbitMqSettings.cs
│   └── MessageQueueSettings.cs
├── Extensions/                # Extension methods
│   ├── ServiceCollectionExtensions.cs
│   └── ConfigurationExtensions.cs
└── Utilities/                 # Legacy utilities (backward compatibility)
    └── RabbitMQService.cs
```

## 🚀 Cách sử dụng

### 1. Thêm Common services vào Program.cs

```csharp
using Common.Extensions;

// Thêm Common services
builder.Services.AddCommonServices(builder.Configuration);

// Thêm database context cụ thể
builder.Services.AddDatabaseContext<YourDbContext>(builder.Configuration, "YourConnectionString");
```

### 2. Cấu hình appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YourDb;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Database": {
    "ConnectionString": "Server=localhost;Database=YourDb;Trusted_Connection=true;TrustServerCertificate=true;",
    "Provider": "SqlServer",
    "CommandTimeout": 30,
    "EnableSensitiveDataLogging": false,
    "EnableRetryOnFailure": true,
    "MaxRetryCount": 3
  },
  "MessageQueue": {
    "Provider": "RabbitMQ",
    "RabbitMQ": {
      "Exchange": "your-exchange",
      "RoutingKey": "your-routing-key",
      "Queue": "your-queue",
      "MaxRetries": 3
    }
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Username": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "Port": 5672
  }
}
```

### 3. Tạo Database Context

```csharp
using Common.Infrastructure.Database;
using Common.Domain.Entities;

public class YourDbContext : BaseDbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }
    
    public DbSet<YourEntity> YourEntities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Your specific configurations
    }
}
```

### 4. Sử dụng Message Queue

```csharp
public class YourService
{
    private readonly IMessageQueueApplication _messageQueue;

    public YourService(IMessageQueueApplication messageQueue)
    {
        _messageQueue = messageQueue;
    }

    public async Task PublishMessageAsync<T>(T message)
    {
        await _messageQueue.PublishMessageAsync("your-exchange", "your-routing-key", message);
    }

    public async Task SubscribeToMessagesAsync<T>(Func<T, Task> handler)
    {
        await _messageQueue.SubscribeToQueueAsync("your-queue", handler);
    }
}
```

### 5. Sử dụng Repository Pattern

```csharp
public class YourService
{
    private readonly IRepository<YourEntity> _repository;

    public YourService(IRepository<YourEntity> repository)
    {
        _repository = repository;
    }

    public async Task<YourEntity> CreateAsync(YourEntity entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task<IEnumerable<YourEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}
```

## 🔧 Features

### Database
- **BaseDbContext**: Base context với audit fields và transaction support
- **BaseRepository**: Generic repository pattern
- **DatabaseContextFactory**: Factory pattern cho context creation
- **Auto audit**: Tự động cập nhật CreatedAt, UpdatedAt
- **Soft delete**: Hỗ trợ soft delete với IsDeleted flag

### Message Queue
- **RabbitMQ**: Full implementation với retry mechanism
- **Kafka**: Placeholder cho future implementation
- **Generic support**: Hỗ trợ publish/subscribe với generic types
- **Error handling**: Retry mechanism và error logging

### Configuration
- **Flexible configuration**: Hỗ trợ multiple providers
- **Settings classes**: Strongly typed configuration
- **Extension methods**: Easy DI registration

## 📦 Dependencies

- Microsoft.EntityFrameworkCore (9.0.8)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.8)
- Microsoft.Extensions.Configuration (9.0.8)
- Microsoft.Extensions.DependencyInjection (9.0.8)
- Microsoft.Extensions.Logging (9.0.8)
- RabbitMQ.Client (7.1.2)
- System.Text.Json (9.0.8)

## 🔄 Migration từ RabbitMQService

Nếu bạn đang sử dụng `RabbitMQService` cũ, bạn có thể:

1. **Giữ nguyên**: `RabbitMQService` vẫn hoạt động và đã được cập nhật để implement `IMessageQueueService`
2. **Migrate dần**: Sử dụng `IMessageQueueService` mới trong code mới
3. **Hybrid**: Sử dụng cả hai trong quá trình transition

## 🧪 Testing

```csharp
// Mock IMessageQueueService
var mockMessageQueue = new Mock<IMessageQueueService>();
mockMessageQueue.Setup(x => x.PublishAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
    .Returns(Task.CompletedTask);

// Mock IDatabaseContext
var mockContext = new Mock<IDatabaseContext>();
```

## 🔍 Logging

Tất cả services đều có logging được tích hợp sẵn:

```csharp
_logger.LogInformation("Message published to exchange {Exchange} with routing key {RoutingKey}", exchange, routingKey);
_logger.LogError(ex, "Failed to publish message to exchange {Exchange}", exchange);
```
