using RegistrationApp.Domain.Common;

namespace RegistrationApp.Domain.Registrations.ValueObjects;

public sealed class PersonalName
{
    private PersonalName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string FullName => string.Join(' ', new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));

    public static ValidationResult<PersonalName> Create(string? firstName, string? lastName)
    {
        var errors = new List<ValidationError>();

        var trimmedFirstName = firstName?.Trim() ?? string.Empty;
        var trimmedLastName = lastName?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(trimmedFirstName))
        {
            errors.Add(ValidationError.Create("Name.FirstName.Empty", "نام باید وارد شود."));
        }
        else if (trimmedFirstName.Length is < 2 or > 100)
        {
            errors.Add(ValidationError.Create("Name.FirstName.Length", "نام باید بین ۲ تا ۱۰۰ کاراکتر باشد."));
        }

        if (string.IsNullOrWhiteSpace(trimmedLastName))
        {
            errors.Add(ValidationError.Create("Name.LastName.Empty", "نام خانوادگی باید وارد شود."));
        }
        else if (trimmedLastName.Length is < 2 or > 100)
        {
            errors.Add(ValidationError.Create("Name.LastName.Length", "نام خانوادگی باید بین ۲ تا ۱۰۰ کاراکتر باشد."));
        }

        return errors.Count > 0
            ? ValidationResult<PersonalName>.Failure(errors)
            : ValidationResult<PersonalName>.Success(new PersonalName(trimmedFirstName, trimmedLastName));
    }
}
