using Devices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.Contracts.Devices
{
    public sealed record UpdateDeviceRequest(
        string Name,
        string Brand,
        DeviceState State
    );
}
