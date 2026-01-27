using Devices.Application.Contracts.Devices;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;
using Devices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.UseCases.Devices
{
    public sealed class CreateDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public CreateDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceResponse> ExecuteAsync(CreateDeviceRequest request, CancellationToken ct)
        {
            // Domain should decide CreationTime immutability; we just set on creation.
            var device = new Device(
                request.Name,
                request.Brand,
                request.State
            );

            await _repo.AddAsync(device, ct);
            await _repo.SaveChangesAsync(ct);

            return DeviceMapper.ToResponse(device);
        }
    }
}
