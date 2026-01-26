namespace Devices.Domain.Exceptions
{
    /// <summary>
    /// Thrown when an operation is not allowed because the device is InUse.
    /// </summary>
    public sealed class DeviceInUseException(string message) : DomainException(message);
}
