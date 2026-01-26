namespace Devices.Domain.Exceptions
{
    /// <summary>
    /// Base exception type for domain rule violations.
    /// </summary>
    public abstract class DomainException(string message) : Exception(message);
}


