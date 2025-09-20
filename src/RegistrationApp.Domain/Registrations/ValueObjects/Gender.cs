using RegistrationApp.Domain.Common;

namespace RegistrationApp.Domain.Registrations.ValueObjects;

public sealed class Gender
{
    private static readonly IReadOnlyDictionary<string, string> SupportedValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["female"] = "زن",
        ["male"] = "مرد",
        ["other"] = "سایر",
    };

    private Gender(string key, string displayName)
    {
        Key = key;
        DisplayName = displayName;
    }

    public string Key { get; }

    public string DisplayName { get; }

    public static ValidationResult<Gender?> CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ValidationResult<Gender?>.Success(null);
        }

        var normalized = value.Trim();

        var match = SupportedValues
            .FirstOrDefault(pair => string.Equals(pair.Value, normalized, StringComparison.OrdinalIgnoreCase) || string.Equals(pair.Key, normalized, StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(match.Key))
        {
            return ValidationResult<Gender?>.Failure(new[]
            {
                ValidationError.Create("Gender.Invalid", "گزینه‌ی انتخاب شده برای جنسیت معتبر نیست."),
            });
        }

        return ValidationResult<Gender?>.Success(new Gender(match.Key, match.Value));
    }

    public static IReadOnlyDictionary<string, string> GetDisplayOptions() => SupportedValues;
}
