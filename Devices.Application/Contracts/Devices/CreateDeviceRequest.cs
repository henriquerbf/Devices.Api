using Devices.Domain.Enums;

namespace Devices.Application.Contracts.Devices
{
    public sealed record CreateDeviceRequest(
        string Name,
        string Brand,
        DeviceState State
    );
}
