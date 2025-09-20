using System.Globalization;
using RegistrationApp.Application.Abstractions;
using RegistrationApp.Application.Contracts.Registrations;
using RegistrationApp.Domain.Abstractions;
using RegistrationApp.Domain.Common;
using RegistrationApp.Domain.Registrations;

namespace RegistrationApp.Application.Services;

public sealed class RegistrationService : IRegistrationService
{
    private readonly IClock _clock;

    public RegistrationService(IClock clock)
    {
        _clock = clock;
    }

    public RegistrationResult ValidateAndCreateRecord(RegistrationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var birthDateResult = ParseBirthDate(request.BirthDate);
        if (birthDateResult.IsFailure)
        {
            return RegistrationResult.Failure(birthDateResult.Errors.Select(error => new ValidationErrorDto(error.Code, error.Message)));
        }

        var domainResult = Registrant.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Gender,
            request.City,
            request.Address,
            birthDateResult.Value,
            _clock);

        if (domainResult.IsFailure)
        {
            return RegistrationResult.Failure(domainResult.Errors.Select(error => new ValidationErrorDto(error.Code, error.Message)));
        }

        var registrant = domainResult.Value!;

        return RegistrationResult.Success(new RegistrationRecordDto
        {
            Id = registrant.Id,
            FirstName = registrant.Name.FirstName,
            LastName = registrant.Name.LastName,
            FullName = registrant.Name.FullName,
            Email = registrant.Email.Value,
            PhoneNumber = registrant.PhoneNumber?.Value,
            BirthDate = registrant.BirthDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            GenderKey = registrant.Gender?.Key,
            GenderDisplayName = registrant.Gender?.DisplayName,
            City = registrant.Address?.City,
            Address = registrant.Address?.FullAddress,
            CreatedAt = registrant.CreatedAt.ToString("O", CultureInfo.InvariantCulture)
        });
    }

    private static ValidationResult<DateOnly?> ParseBirthDate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ValidationResult<DateOnly?>.Success(null);
        }

        if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out var birthDate))
        {
            return ValidationResult<DateOnly?>.Success(birthDate);
        }

        return ValidationResult<DateOnly?>.Failure(new[]
        {
            ValidationError.Create("BirthDate.Invalid", "قالب تاریخ تولد معتبر نیست."),
        });
    }
}
