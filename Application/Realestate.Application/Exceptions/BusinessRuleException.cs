namespace Realestate.Application.Exceptions;

/// <summary>
/// Thrown when a business rule is violated (e.g., duplicate entry, invalid state transition).
/// </summary>
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }

    public BusinessRuleException(string message, Exception innerException) : base(message, innerException) { }
}