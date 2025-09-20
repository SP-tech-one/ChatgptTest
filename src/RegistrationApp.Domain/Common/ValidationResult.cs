namespace RegistrationApp.Domain.Common;

public sealed class ValidationResult<T>
{
    private ValidationResult(T value)
    {
        IsSuccess = true;
        Value = value;
        Errors = Array.Empty<ValidationError>();
    }

    private ValidationResult(IReadOnlyCollection<ValidationError> errors)
    {
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public IReadOnlyCollection<ValidationError> Errors { get; } = Array.Empty<ValidationError>();

    public static ValidationResult<T> Success(T value) => new(value);

    public static ValidationResult<T> Failure(IEnumerable<ValidationError> errors)
    {
        var errorArray = errors.ToArray();
        if (errorArray.Length == 0)
        {
            throw new ArgumentException("At least one error is required for a failure result.", nameof(errors));
        }

        return new ValidationResult<T>(errorArray);
    }
}
