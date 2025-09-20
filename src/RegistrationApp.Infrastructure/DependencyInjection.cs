using Microsoft.Extensions.DependencyInjection;
using RegistrationApp.Application.Abstractions;
using RegistrationApp.Domain.Abstractions;
using RegistrationApp.Infrastructure.Registrations;
using RegistrationApp.Infrastructure.Time;

namespace RegistrationApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IRegistrationFormDefinitionProvider, RegistrationFormDefinitionProvider>();
        return services;
    }
}
