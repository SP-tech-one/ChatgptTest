namespace RegistrationApp.Application.Contracts.Forms;

public sealed class RegistrationFormDefinition
{
    public required string Title { get; init; }

    public string? Description { get; init; }

    public IReadOnlyList<FormSectionDefinition> Sections { get; init; } = Array.Empty<FormSectionDefinition>();
}
