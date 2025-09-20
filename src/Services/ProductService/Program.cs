using Common.Application.Interfaces;
using Common.Application.Services;
using Common.Configurations;
using Common.Extensions;
using ProductService.Consumer;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Data;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Common services
builder.Services.AddCommonServices(builder.Configuration);

// Add Database Context
builder.Services.AddDatabaseContext<ProductDbContext>(builder.Configuration, "ProductConnection");

// Configure RabbitMQ
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

// Register services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductAppService>();
builder.Services.AddHostedService<ProductConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
