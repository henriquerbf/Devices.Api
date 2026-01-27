using Devices.Application.Contracts.Devices;
using Devices.Domain.Entities;

namespace Devices.Application.Mappers
{
    public static class DeviceMapper
    {
        public static DeviceResponse ToResponse(Device device)
            => new(
                device.Id,
                device.Name,
                device.Brand,
                device.State,
                device.CreationTime
            );
    }
}
