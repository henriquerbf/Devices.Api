using Devices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.Contracts.Devices
{
    public sealed record DeviceResponse(
        Guid Id,
        string Name,
        string Brand,
        DeviceState State,
        DateTimeOffset CreationTime
    );
}
