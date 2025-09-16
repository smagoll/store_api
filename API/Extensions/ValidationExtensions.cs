using FluentValidation;

namespace API.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
        return services;
    }
}