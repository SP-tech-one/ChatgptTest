namespace RegistrationApp.Domain.Common;

public sealed record ValidationError(string Code, string Message)
{
    public static ValidationError Create(string code, string message) => new(code, message);
}
