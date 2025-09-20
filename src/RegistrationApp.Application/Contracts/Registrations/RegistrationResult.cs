namespace RegistrationApp.Application.Contracts.Registrations;

public sealed class RegistrationResult
{
    private RegistrationResult(RegistrationRecordDto record)
    {
        Record = record;
        Errors = Array.Empty<ValidationErrorDto>();
        Succeeded = true;
    }

    private RegistrationResult(IReadOnlyCollection<ValidationErrorDto> errors)
    {
        Errors = errors;
    }

    public bool Succeeded { get; }

    public bool Failed => !Succeeded;

    public RegistrationRecordDto? Record { get; }

    public IReadOnlyCollection<ValidationErrorDto> Errors { get; } = Array.Empty<ValidationErrorDto>();

    public static RegistrationResult Success(RegistrationRecordDto record) => new(record);

    public static RegistrationResult Failure(IEnumerable<ValidationErrorDto> errors)
    {
        var materialized = errors.ToArray();
        if (materialized.Length == 0)
        {
            throw new ArgumentException("Errors collection cannot be empty for a failure result.", nameof(errors));
        }

        return new RegistrationResult(materialized);
    }
}
