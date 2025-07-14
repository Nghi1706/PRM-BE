// Api/Extensions/Program.cs
using RestaurantManagement.Application;
using RestaurantManagement.Infrastructure;
using RestaurantManagement.Api.Endpoints;
using RestaurantManagement.Application.Services;


// Api/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Config PORT rõ ràng
builder.WebHost.UseUrls("http://localhost:5000");

// Add Services
builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure Middleware rõ ràng
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

// Map Routes tách riêng
app.MapGroup("/api")
   .MapAppEndpoints();

app.Run();