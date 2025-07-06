// Api/Extensions/WebApplicationBuilderExtensions.cs
public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        return services;
    }
}
