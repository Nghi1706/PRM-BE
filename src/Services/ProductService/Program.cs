using Common.Utilities;
using Common.Configurations;
using ProductService.Consumer;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure RabbitMQ
builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMQSettings"));

builder.Services.AddScoped<IProductService, ProductAppService>();
builder.Services.AddSingleton<RabbitMqConfigHelper>();
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
