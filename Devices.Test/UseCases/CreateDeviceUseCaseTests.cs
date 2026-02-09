using Devices.Application.Contracts.Devices;
using Devices.Application.UseCases.Devices;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class CreateDeviceUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_CreatesDevice_ReturnsResponse()
        {
            var repo = new FakeDeviceRepository();
            var useCase = new CreateDeviceUseCase(repo);

            var req = new CreateDeviceRequest("Mouse", "Logitech", DeviceState.Available);

            var resp = await useCase.ExecuteAsync(req, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, resp.Id);
            Assert.Equal("Mouse", resp.Name);
            Assert.Equal("Logitech", resp.Brand);
            Assert.Equal(DeviceState.Available, resp.State);
            Assert.True(resp.CreationTime <= DateTimeOffset.UtcNow);
        }
    }
}