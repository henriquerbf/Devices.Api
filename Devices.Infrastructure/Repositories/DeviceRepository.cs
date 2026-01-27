using Devices.Application.Interfaces.Repositories;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Devices.Infrastructure.Repositories
{
    public sealed class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDbContext _db;

        public DeviceRepository(DeviceDbContext db) => _db = db;

        public async Task<Device?> GetByIdAsync(Guid id, CancellationToken ct)
            => await _db.Devices.FirstOrDefaultAsync(x => x.Id == id, ct);

        public async Task<IReadOnlyList<Device>> GetAllAsync(CancellationToken ct)
            => await _db.Devices.AsNoTracking().OrderByDescending(x => x.CreationTime).ToListAsync(ct);

        public async Task<IReadOnlyList<Device>> GetByBrandAsync(string brand, CancellationToken ct)
            => await _db.Devices.AsNoTracking()
                .Where(x => x.Brand == brand)
                .OrderByDescending(x => x.CreationTime)
                .ToListAsync(ct);

        public async Task<IReadOnlyList<Device>> GetByStateAsync(DeviceState state, CancellationToken ct)
            => await _db.Devices.AsNoTracking()
                .Where(x => x.State == state)
                .OrderByDescending(x => x.CreationTime)
                .ToListAsync(ct);

        public async Task AddAsync(Device device, CancellationToken ct)
            => await _db.Devices.AddAsync(device, ct);

        public void Update(Device device)
            => _db.Devices.Update(device);

        public void Remove(Device device)
            => _db.Devices.Remove(device);

        public async Task<int> SaveChangesAsync(CancellationToken ct)
            => await _db.SaveChangesAsync(ct);
    }
}
