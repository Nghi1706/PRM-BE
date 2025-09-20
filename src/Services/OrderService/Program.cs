using Common.Configurations;
using Common.Extensions;
using Common.Infrastructure.MessageQueue.RabbitMQ;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Data;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://127.0.0.1:5014");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Common services
builder.Services.AddCommonServices(builder.Configuration);

// Add Database Context
builder.Services.AddDatabaseContext<OrderDbContext>(builder.Configuration, "OrderConnection");

// Configure RabbitMQ
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddSingleton<RabbitMQService>();

// Register services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderService, OrderAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();