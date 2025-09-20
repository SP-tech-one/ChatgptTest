using Microsoft.Extensions.DependencyInjection;
using RegistrationApp.Application.Abstractions;
using RegistrationApp.Application.Services;

namespace RegistrationApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRegistrationService, RegistrationService>();
        return services;
    }
}
