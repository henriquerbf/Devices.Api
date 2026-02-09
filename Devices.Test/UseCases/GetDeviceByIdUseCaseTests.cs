using Devices.Application.Exceptions;
using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class GetDeviceByIdUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_WhenNotFound_ThrowsNotFoundException()
        {
            var repo = new FakeDeviceRepository();
            var useCase = new GetDeviceByIdUseCase(repo);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                useCase.ExecuteAsync(Guid.NewGuid(), CancellationToken.None));
        }

        [Fact]
        public async Task ExecuteAsync_WhenExists_ReturnsDeviceResponse()
        {
            var repo = new FakeDeviceRepository();
            var device = new Device("Keyboard", "Keychron", DeviceState.Available);
            repo.Seed(device);

            var useCase = new GetDeviceByIdUseCase(repo);

            var resp = await useCase.ExecuteAsync(device.Id, CancellationToken.None);

            Assert.Equal(device.Id, resp.Id);
            Assert.Equal("Keyboard", resp.Name);
            Assert.Equal("Keychron", resp.Brand);
        }
    }
}