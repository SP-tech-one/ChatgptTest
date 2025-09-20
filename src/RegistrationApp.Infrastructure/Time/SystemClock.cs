using RegistrationApp.Domain.Abstractions;

namespace RegistrationApp.Infrastructure.Time;

public sealed class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
