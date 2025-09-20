namespace RegistrationApp.Application.Contracts.Registrations;

public sealed class RegistrationRecordDto
{
    public Guid Id { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string FullName { get; init; }

    public required string Email { get; init; }

    public string? PhoneNumber { get; init; }

    public string? BirthDate { get; init; }

    public string? GenderKey { get; init; }

    public string? GenderDisplayName { get; init; }

    public string? City { get; init; }

    public string? Address { get; init; }

    public required string CreatedAt { get; init; }
}
