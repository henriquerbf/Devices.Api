using Devices.Application.Exceptions;
using Devices.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.UseCases.Devices
{
    public sealed class DeleteDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public DeleteDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task ExecuteAsync(Guid id, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(id, ct);
            if (device is null)
                throw new NotFoundException($"Device '{id}' was not found.");

            // Domain should enforce: in-use cannot be deleted (device.Delete() throws, or rule method)
            device.EnsureDeletable();

            _repo.Remove(device);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
