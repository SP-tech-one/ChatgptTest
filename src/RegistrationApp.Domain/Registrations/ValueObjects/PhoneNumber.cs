using System.Text.RegularExpressions;
using RegistrationApp.Domain.Common;

namespace RegistrationApp.Domain.Registrations.ValueObjects;

public sealed class PhoneNumber
{
    private static readonly Regex DigitsRegex = new("^[0-9]{10,15}$", RegexOptions.Compiled);

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static ValidationResult<PhoneNumber?> CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ValidationResult<PhoneNumber?>.Success(null);
        }

        var normalized = value.Trim().Replace(" ", string.Empty).Replace("-", string.Empty);

        if (!DigitsRegex.IsMatch(normalized))
        {
            return ValidationResult<PhoneNumber?>.Failure(new[]
            {
                ValidationError.Create("PhoneNumber.Invalid", "شماره تماس باید بین ۱۰ تا ۱۵ رقم و فقط شامل اعداد باشد."),
            });
        }

        return ValidationResult<PhoneNumber?>.Success(new PhoneNumber(normalized));
    }
}
