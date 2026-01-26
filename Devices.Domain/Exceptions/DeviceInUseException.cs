namespace Devices.Domain.Exceptions
{
    public sealed class DeviceInUseException(string message) : DomainException(message);
}
