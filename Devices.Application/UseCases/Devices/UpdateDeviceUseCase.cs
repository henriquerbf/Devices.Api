using Devices.Application.Contracts.Devices;
using Devices.Application.Exceptions;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.UseCases.Devices
{
    public sealed class UpdateDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public UpdateDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceResponse> ExecuteAsync(Guid id, UpdateDeviceRequest request, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(id, ct);
            if (device is null)
                throw new NotFoundException($"Device '{id}' was not found.");

            // Domain should enforce: creationTime immutable, cannot change name/brand if in-use, etc.
            device.UpdateName(request.Name);
            device.UpdateBrand(request.Brand);
            device.ChangeState(request.State);

            _repo.Update(device);
            await _repo.SaveChangesAsync(ct);

            return DeviceMapper.ToResponse(device);
        }
    }
}
