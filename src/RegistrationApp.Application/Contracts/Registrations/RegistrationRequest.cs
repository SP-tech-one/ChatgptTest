namespace RegistrationApp.Application.Contracts.Registrations;

public sealed class RegistrationRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }
}
