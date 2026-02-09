using Devices.Application.Interfaces.Repositories;
using Devices.Domain.Entities;
using Devices.Domain.Enums;

namespace Devices.UnitTests.Fakes
{
    public sealed class FakeDeviceRepository : IDeviceRepository
    {
        private readonly List<Device> _devices = [];

        public Task<Device?> GetByIdAsync(Guid id, CancellationToken ct)
            => Task.FromResult(_devices.FirstOrDefault(x => x.Id == id));

        public Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken ct)
            => Task.FromResult((IReadOnlyList<Device>)_devices.ToList());

        public Task<IReadOnlyList<Device>> GetByBrandAsync(string brand, CancellationToken ct)
            => Task.FromResult((IReadOnlyList<Device>)_devices.Where(x => x.Brand == brand).ToList());

        public Task<IReadOnlyList<Device>> GetByStateAsync(DeviceState state, CancellationToken ct)
            => Task.FromResult((IReadOnlyList<Device>)_devices.Where(x => x.State == state).ToList());

        public Task AddAsync(Device device, CancellationToken ct)
        {
            _devices.Add(device);
            return Task.CompletedTask;
        }

        public void Update(Device device) { /* no-op for fake */ }

        public void Remove(Device device) => _devices.Remove(device);

        public Task<int> SaveChangesAsync(CancellationToken ct) => Task.FromResult(1);

        // helper for tests
        public void Seed(Device device) => _devices.Add(device);
    }
}
