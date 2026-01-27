using Devices.Application.Contracts.Devices;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;

namespace Devices.Application.UseCases.Devices
{
    public sealed class GetDevicesByBrandUseCase
    {
        private readonly IDeviceRepository _repo;

        public GetDevicesByBrandUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<DeviceResponse>> ExecuteAsync(string brand, CancellationToken ct)
            => (await _repo.GetByBrandAsync(brand, ct)).Select(DeviceMapper.ToResponse).ToList();
    }
}
