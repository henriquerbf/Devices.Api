using Devices.Application.Contracts.Devices;
using Devices.Application.Interfaces.Repositories;
using Devices.Application.Mappers;
using Devices.Domain.Enums;

namespace Devices.Application.UseCases.Devices
{
    public sealed class GetDevicesByStateUseCase
    {
        private readonly IDeviceRepository _repo;

        public GetDevicesByStateUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<DeviceResponse>> ExecuteAsync(DeviceState state, CancellationToken ct)
            => (await _repo.GetByStateAsync(state, ct)).Select(DeviceMapper.ToResponse).ToList();
    }
}
