using RegistrationApp.Application.Contracts.Forms;

namespace RegistrationApp.Application.Abstractions;

public interface IRegistrationFormDefinitionProvider
{
    RegistrationFormDefinition GetDefinition();
}
