using Devices.Application.Interfaces;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Devices.Infrastructure.Seeding
{
    public sealed class DeviceDataSeeder : IDataSeeder
    {
        private readonly DeviceDbContext _db;

        public DeviceDataSeeder(DeviceDbContext db) => _db = db;

        public async Task SeedAsync(CancellationToken ct)
        {
            // Idempotent: if there is any data, do nothing.
            List<Device> existingDevices = await _db.Devices.ToListAsync(ct);
            if (await _db.Devices.AnyAsync(ct))
                return;

            var now = DateTimeOffset.UtcNow;

            var seed = new List<Device>
            {
                // =========================
                // Apple
                // =========================
                new Device("iPhone 15 Pro", "Apple", DeviceState.Available),
                new Device("iPhone 14", "Apple", DeviceState.InUse),
                new Device("MacBook Pro M3", "Apple", DeviceState.Available),
                new Device("iPad Air", "Apple", DeviceState.Inactive),

                // =========================
                // Samsung
                // =========================
                new Device("Galaxy S24 Ultra", "Samsung", DeviceState.Available),
                new Device("Galaxy S23", "Samsung", DeviceState.InUse),
                new Device("Galaxy Tab S9", "Samsung", DeviceState.Available),

                // =========================
                // Amazon
                // =========================
                new Device("Kindle Paperwhite", "Amazon", DeviceState.Available),
                new Device("Echo Dot 5th Gen", "Amazon", DeviceState.Inactive),

                // =========================
                // Google
                // =========================
                new Device("Pixel 8 Pro", "Google", DeviceState.Available),
                new Device("Pixel Watch 2", "Google", DeviceState.InUse),

                // =========================
                // Microsoft
                // =========================
                new Device("Surface Pro 9", "Microsoft", DeviceState.InUse),
                new Device("Xbox Series X", "Microsoft", DeviceState.Inactive),

                // =========================
                // Dell
                // =========================
                new Device("XPS 13 Plus", "Dell", DeviceState.Available),
                new Device("Alienware m18", "Dell", DeviceState.InUse),

                // =========================
                // Sony
                // =========================
                new Device("PlayStation 5", "Sony", DeviceState.Available),
                new Device("WH-1000XM5", "Sony", DeviceState.Inactive)
            };

            await _db.Devices.AddRangeAsync(seed, ct);
            await _db.SaveChangesAsync(ct);
        }
    }
}
