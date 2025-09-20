using RegistrationApp.Domain.Abstractions;
using RegistrationApp.Domain.Common;
using RegistrationApp.Domain.Registrations.ValueObjects;

namespace RegistrationApp.Domain.Registrations;

public sealed class Registrant
{
    private Registrant(
        Guid id,
        PersonalName name,
        EmailAddress email,
        PhoneNumber? phoneNumber,
        Gender? gender,
        Address? address,
        DateOnly? birthDate,
        DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Gender = gender;
        Address = address;
        BirthDate = birthDate;
        CreatedAt = createdAt;
    }

    public Guid Id { get; }

    public PersonalName Name { get; }

    public EmailAddress Email { get; }

    public PhoneNumber? PhoneNumber { get; }

    public Gender? Gender { get; }

    public Address? Address { get; }

    public DateOnly? BirthDate { get; }

    public DateTimeOffset CreatedAt { get; }

    public static ValidationResult<Registrant> Create(
        string? firstName,
        string? lastName,
        string? email,
        string? phoneNumber,
        string? gender,
        string? city,
        string? address,
        DateOnly? birthDate,
        IClock clock)
    {
        ArgumentNullException.ThrowIfNull(clock);

        var errors = new List<ValidationError>();

        var nameResult = PersonalName.Create(firstName, lastName);
        if (nameResult.IsFailure)
        {
            errors.AddRange(nameResult.Errors);
        }

        var emailResult = EmailAddress.Create(email);
        if (emailResult.IsFailure)
        {
            errors.AddRange(emailResult.Errors);
        }

        var phoneResult = PhoneNumber.CreateOptional(phoneNumber);
        if (phoneResult.IsFailure)
        {
            errors.AddRange(phoneResult.Errors);
        }

        var genderResult = Gender.CreateOptional(gender);
        if (genderResult.IsFailure)
        {
            errors.AddRange(genderResult.Errors);
        }

        var addressResult = Address.CreateOptional(city, address);
        if (addressResult.IsFailure)
        {
            errors.AddRange(addressResult.Errors);
        }

        if (birthDate is { } birth)
        {
            var today = DateOnly.FromDateTime(clock.UtcNow.Date);
            if (birth > today)
            {
                errors.Add(ValidationError.Create("BirthDate.Future", "تاریخ تولد نمی‌تواند در آینده باشد."));
            }
            else if (birth < today.AddYears(-120))
            {
                errors.Add(ValidationError.Create("BirthDate.TooOld", "تاریخ تولد وارد شده معتبر نیست."));
            }
        }

        if (errors.Count > 0)
        {
            return ValidationResult<Registrant>.Failure(errors);
        }

        return ValidationResult<Registrant>.Success(new Registrant(
            Guid.NewGuid(),
            nameResult.Value!,
            emailResult.Value!,
            phoneResult.Value,
            genderResult.Value,
            addressResult.Value,
            birthDate,
            clock.UtcNow));
    }
}
