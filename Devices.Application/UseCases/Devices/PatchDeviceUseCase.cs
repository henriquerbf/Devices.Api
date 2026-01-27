using Devices.Application.Contracts.Devices;
using Devices.Application.Exceptions;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.UseCases.Devices
{
    public sealed class PatchDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public PatchDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceResponse> ExecuteAsync(Guid id, PatchDeviceRequest request, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(id, ct);
            if (device is null)
                throw new NotFoundException($"Device '{id}' was not found.");

            // Apply only provided fields
            if (request.Name is not null) device.UpdateName(request.Name);
            if (request.Brand is not null) device.UpdateBrand(request.Brand);
            if (request.State is not null) device.ChangeState(request.State.Value);

            _repo.Update(device);
            await _repo.SaveChangesAsync(ct);

            return DeviceMapper.ToResponse(device);
        }
    }
}
