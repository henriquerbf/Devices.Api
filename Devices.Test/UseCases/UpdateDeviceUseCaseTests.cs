using Devices.Application.Contracts.Devices;
using Devices.Application.Exceptions;
using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class UpdateDeviceUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_WhenNotFound_ThrowsNotFoundException()
        {
            var repo = new FakeDeviceRepository();
            var useCase = new UpdateDeviceUseCase(repo);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                useCase.ExecuteAsync(Guid.NewGuid(), new UpdateDeviceRequest("n","b", DeviceState.Available), CancellationToken.None));
        }

        [Fact]
        public async Task ExecuteAsync_WhenExists_UpdatesAndReturnsResponse()
        {
            var repo = new FakeDeviceRepository();
            var device = new Device("Old", "OldBrand", DeviceState.Available);
            repo.Seed(device);

            var useCase = new UpdateDeviceUseCase(repo);
            var req = new UpdateDeviceRequest("NewName", "NewBrand", DeviceState.Inactive);

            var resp = await useCase.ExecuteAsync(device.Id, req, CancellationToken.None);

            Assert.Equal(device.Id, resp.Id);
            Assert.Equal("NewName", resp.Name);
            Assert.Equal("NewBrand", resp.Brand);
            Assert.Equal(DeviceState.Inactive, resp.State);
        }
    }
}