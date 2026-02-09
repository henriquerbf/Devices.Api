using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class GetDevicesByStateUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_FiltersByState()
        {
            var repo = new FakeDeviceRepository();
            repo.Seed(new Device("A1", "Apple", DeviceState.InUse));
            repo.Seed(new Device("A2", "Apple", DeviceState.Available));

            var useCase = new GetDevicesByStateUseCase(repo);

            var list = await useCase.ExecuteAsync(DeviceState.InUse, CancellationToken.None);

            Assert.Single(list);
            Assert.Equal(DeviceState.InUse, list[0].State);
        }
    }
}