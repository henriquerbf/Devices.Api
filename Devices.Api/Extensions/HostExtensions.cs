using Devices.Application.Interfaces;
using Devices.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Extensions
{
    public static class HostExtensions
    {
        public static async Task ApplyMigrationsAndSeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<DeviceDbContext>();
            await db.Database.MigrateAsync();

            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedAsync(CancellationToken.None);
        }
    }
}
