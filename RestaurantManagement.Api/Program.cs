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


// Api/Program.cs
var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load(); // Loads from root directory by default

// JWT config từ environment variables với fallbacks
var jwtKey = builder.Configuration["Jwt:Key"] ?? 
             "error-config";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? 
                "error-config"; 
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? 
                  "error-config";

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

// Config PORT 
builder.WebHost.UseUrls("http://localhost:5000");

// Add Services
builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5121", "https://localhost:7134", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantManagement API", Version = "v1" });
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

// Configure Middleware rõ ràng
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantManagement API v1");
    options.RoutePrefix = "swagger";
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    options.DefaultModelsExpandDepth(-1);
    
    // Show UI authorize
    options.DisplayRequestDuration();
    options.EnableDeepLinking();
    options.EnableFilter();
    options.ShowExtensions();
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Map Routes tách riêng
app.MapGroup("/api")
   .MapAppEndpoints();

app.Run();