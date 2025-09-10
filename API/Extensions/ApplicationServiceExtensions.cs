using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOrderService, OrderService>();
            
            
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();

        // Mapping
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}