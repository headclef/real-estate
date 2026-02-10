namespace Realestate.Application.Exceptions;

/// <summary>
/// Thrown when input validation fails before processing a request.
/// Carries the list of validation error messages.
/// </summary>
public class ValidationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public ValidationException(string error) : this(new[] { error }) { }

    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
        Errors = new[] { message };
    }
}