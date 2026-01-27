using Devices.Application.Interfaces;
using Devices.Application.Interfaces.Repositories;
using Devices.Infrastructure.Persistance;
using Devices.Infrastructure.Repositories;
using Devices.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DevicesDb")
                ?? throw new InvalidOperationException(
                    "Connection string 'DevicesDb' not found.");

            services.AddDbContext<DeviceDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });
            });

            // Repositories (Outbound Adapters)
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            // Seeding
            services.AddScoped<IDataSeeder, DeviceDataSeeder>();

            return services;
        }
    }
}
