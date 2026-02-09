using Devices.Application.Contracts.Devices;
using Devices.Application.Exceptions;
using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class PatchDeviceUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_WhenNotFound_ThrowsNotFoundException()
        {
            var repo = new FakeDeviceRepository();
            var useCase = new PatchDeviceUseCase(repo);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                useCase.ExecuteAsync(Guid.NewGuid(), new PatchDeviceRequest("n", null, null), CancellationToken.None));
        }

        [Fact]
        public async Task ExecuteAsync_WhenExists_PatchesProvidedFields()
        {
            var repo = new FakeDeviceRepository();
            var device = new Device("Name", "Brand", DeviceState.Available);
            repo.Seed(device);

            var useCase = new PatchDeviceUseCase(repo);
            var req = new PatchDeviceRequest("PatchedName", null, null);

            var resp = await useCase.ExecuteAsync(device.Id, req, CancellationToken.None);

            Assert.Equal("PatchedName", resp.Name);
            Assert.Equal(device.Brand, resp.Brand);
        }
    }
}