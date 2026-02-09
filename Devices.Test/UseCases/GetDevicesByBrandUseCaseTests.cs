using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class GetDevicesByBrandUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_FiltersByBrand()
        {
            var repo = new FakeDeviceRepository();
            repo.Seed(new Device("A1", "Apple", DeviceState.Available));
            repo.Seed(new Device("S1", "Samsung", DeviceState.Available));

            var useCase = new GetDevicesByBrandUseCase(repo);

            var list = await useCase.ExecuteAsync("Apple", CancellationToken.None);

            Assert.Single(list);
            Assert.Equal("Apple", list[0].Brand);
        }
    }
}