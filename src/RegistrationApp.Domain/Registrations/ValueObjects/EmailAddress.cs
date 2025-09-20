using System.Net.Mail;
using RegistrationApp.Domain.Common;

namespace RegistrationApp.Domain.Registrations.ValueObjects;

public sealed class EmailAddress
{
    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static ValidationResult<EmailAddress> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return ValidationResult<EmailAddress>.Failure(new[]
            {
                ValidationError.Create("Email.Empty", "ایمیل باید وارد شود."),
            });
        }

        var trimmed = value.Trim();

        try
        {
            _ = new MailAddress(trimmed);
        }
        catch (FormatException)
        {
            return ValidationResult<EmailAddress>.Failure(new[]
            {
                ValidationError.Create("Email.Invalid", "ایمیل وارد شده معتبر نیست."),
            });
        }

        if (trimmed.Length > 200)
        {
            return ValidationResult<EmailAddress>.Failure(new[]
            {
                ValidationError.Create("Email.Length", "ایمیل نباید بیش از ۲۰۰ کاراکتر باشد."),
            });
        }

        return ValidationResult<EmailAddress>.Success(new EmailAddress(trimmed));
    }
}
