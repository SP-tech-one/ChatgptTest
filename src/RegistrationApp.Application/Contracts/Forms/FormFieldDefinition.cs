namespace RegistrationApp.Application.Contracts.Forms;

public sealed class FormFieldDefinition
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Label { get; init; }

    public required string Type { get; init; }

    public bool IsRequired { get; init; }

    public string? Placeholder { get; init; }

    public string? HelpText { get; init; }

    public int ColumnSpan { get; init; } = 12;

    public IReadOnlyList<FormFieldOption> Options { get; init; } = Array.Empty<FormFieldOption>();

    public string? CssClass { get; init; }
}
