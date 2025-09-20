namespace RegistrationApp.Domain.Abstractions;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
