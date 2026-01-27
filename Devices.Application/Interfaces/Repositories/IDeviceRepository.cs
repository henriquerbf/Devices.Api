using Devices.Domain.Entities;
using Devices.Domain.Enums;

namespace Devices.Application.Interfaces.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken ct);
        Task<IReadOnlyList<Device>> GetByBrandAsync(string brand, CancellationToken ct);
        Task<IReadOnlyList<Device>> GetByStateAsync(DeviceState state, CancellationToken ct);

        Task AddAsync(Device device, CancellationToken ct);
        void Update(Device device);
        void Remove(Device device);

        Task<int> SaveChangesAsync(CancellationToken ct);
    }

}
