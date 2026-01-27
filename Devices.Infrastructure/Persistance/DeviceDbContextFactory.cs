using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Devices.Infrastructure.Persistance
{
    public sealed class DeviceDbContextFactory : IDesignTimeDbContextFactory<DeviceDbContext>
    {
        public DeviceDbContext CreateDbContext(string[] args)
        {
            // Used only by EF tooling (dotnet ef). Keep it minimal.
            var cs =
                "Server=localhost,1433;" +
                "Database=DevicesDb;" +
                "User Id=sa;" +
                "Password=teste@123;" +
                "TrustServerCertificate=True;" +
                "Encrypt=False;";

            var options = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseSqlServer(cs)
                .Options;

            return new DeviceDbContext(options);
        }
    }
}
