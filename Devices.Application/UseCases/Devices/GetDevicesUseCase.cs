using Devices.Application.Contracts.Devices;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;

namespace Devices.Application.UseCases.Devices
{
    public sealed class GetDevicesUseCase
    {
        private readonly IDeviceRepository _repo;

        public GetDevicesUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<DeviceResponse>> ExecuteAsync(CancellationToken ct)
            => (await _repo.GetAllAsync(ct)).Select(DeviceMapper.ToResponse).ToList();
    }
}
