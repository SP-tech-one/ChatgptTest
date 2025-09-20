using RegistrationApp.Domain.Common;

namespace RegistrationApp.Domain.Registrations.ValueObjects;

public sealed class Address
{
    private Address(string? city, string? fullAddress)
    {
        City = city;
        FullAddress = fullAddress;
    }

    public string? City { get; }

    public string? FullAddress { get; }

    public static ValidationResult<Address?> CreateOptional(string? city, string? fullAddress)
    {
        var trimmedCity = string.IsNullOrWhiteSpace(city) ? null : city.Trim();
        var trimmedAddress = string.IsNullOrWhiteSpace(fullAddress) ? null : fullAddress.Trim();

        var errors = new List<ValidationError>();

        if (trimmedCity is { Length: > 100 })
        {
            errors.Add(ValidationError.Create("Address.City.Length", "نام شهر نباید بیش از ۱۰۰ کاراکتر باشد."));
        }

        if (trimmedAddress is { Length: > 500 })
        {
            errors.Add(ValidationError.Create("Address.Full.Length", "آدرس نباید بیش از ۵۰۰ کاراکتر باشد."));
        }

        if (errors.Count > 0)
        {
            return ValidationResult<Address?>.Failure(errors);
        }

        if (trimmedCity is null && trimmedAddress is null)
        {
            return ValidationResult<Address?>.Success(null);
        }

        return ValidationResult<Address?>.Success(new Address(trimmedCity, trimmedAddress));
    }
}
