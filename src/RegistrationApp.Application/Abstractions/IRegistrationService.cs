using RegistrationApp.Application.Contracts.Registrations;

namespace RegistrationApp.Application.Abstractions;

public interface IRegistrationService
{
    RegistrationResult ValidateAndCreateRecord(RegistrationRequest request);
}
