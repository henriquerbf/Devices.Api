using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class GetDevicesUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsAllDevices()
        {
            var repo = new FakeDeviceRepository();
            repo.Seed(new Device("D1", "X", DeviceState.Available));
            repo.Seed(new Device("D2", "Y", DeviceState.InUse));

            var useCase = new GetDevicesUseCase(repo);

            var list = await useCase.ExecuteAsync(CancellationToken.None);

            Assert.Equal(2, list.Count);
        }
    }
}