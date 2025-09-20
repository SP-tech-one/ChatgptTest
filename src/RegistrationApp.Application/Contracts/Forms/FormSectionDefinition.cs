namespace RegistrationApp.Application.Contracts.Forms;

public sealed class FormSectionDefinition
{
    public required string Title { get; init; }

    public string? Description { get; init; }

    public IReadOnlyList<FormFieldDefinition> Fields { get; init; } = Array.Empty<FormFieldDefinition>();
}
