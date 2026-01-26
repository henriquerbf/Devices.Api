namespace Devices.Domain.Exceptions
{
    public sealed class ImmutableFieldException(string message) : DomainException(message);
}
