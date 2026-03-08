using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceExtensions).Assembly));
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();

        // Mapping
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}