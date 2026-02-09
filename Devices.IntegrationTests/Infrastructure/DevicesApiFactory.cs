using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Devices.IntegrationTests.Infrastructure
{
    public sealed class DevicesApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _connectionString;

        public DevicesApiFactory(string connectionString)
            => _connectionString = connectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureAppConfiguration((_, config) =>
            {
                var settings = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DevicesDb"] = _connectionString
                };

                config.AddInMemoryCollection(settings);
            });
        }
    }
}
