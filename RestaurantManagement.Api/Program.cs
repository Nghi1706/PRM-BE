// Api/Extensions/Program.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Application;
using RestaurantManagement.Infrastructure;
using RestaurantManagement.Api.Endpoints;
using RestaurantManagement.Application.Services;
using System.Text;
using Microsoft.OpenApi.Models;
using DotNetEnv;
using Microsoft.Extensions.Options;
using RestaurantManagement.Application.Settings;
using Microsoft.AspNetCore.Http.Features;
using RestaurantManagement.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

var jwtKey = builder.Configuration["Jwt:Key"] ?? "error-config";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "error-config";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "error-config";

// Register JwtSettings as a service
builder.Services.Configure<JwtSettings>(options =>
{
    options.Key = jwtKey;
    options.Issuer = jwtIssuer;
    options.Audience = jwtAudience;
    options.AccessTokenExpiryHours = 12;
    options.RefreshTokenExpiryDays = 7;
});

// JWT config
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

// THAY ĐỔI TẠI ĐÂY - Config để listen trên tất cả IP
// builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Add Services
builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Configure multipart form options for file upload
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB limit
});

// CẬP NHẬT CORS - Cho phép tất cả IP truy cập
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // Cho phép tất cả IP
              .AllowAnyHeader()
              .AllowAnyMethod();
        // Bỏ .AllowCredentials() khi dùng AllowAnyOrigin()
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantManagement API", Version = "v1" });

    // Add file upload support for Swagger
    options.OperationFilter<FileUploadOperationFilter>();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIs...\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure Middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantManagement API v1");
    options.RoutePrefix = "swagger";
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    options.DefaultModelsExpandDepth(-1);
    options.DisplayRequestDuration();
    options.EnableDeepLinking();
    options.EnableFilter();
    options.ShowExtensions();
});

// BỎ HTTPS redirect để dễ test từ HTTP
// app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Map Routes
app.MapGroup("/api")
   .MapAppEndpoints();

app.Run();