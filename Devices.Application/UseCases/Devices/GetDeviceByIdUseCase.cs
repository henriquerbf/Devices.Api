using Devices.Application.Contracts.Devices;
using Devices.Application.Exceptions;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;

namespace Devices.Application.UseCases.Devices
{
    public sealed class GetDeviceByIdUseCase
    {
        private readonly IDeviceRepository _repo;

        public GetDeviceByIdUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceResponse> ExecuteAsync(Guid id, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(id, ct);
            if (device is null)
                throw new NotFoundException($"Device '{id}' was not found.");

            return DeviceMapper.ToResponse(device);
        }
    }
}
