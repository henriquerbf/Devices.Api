using Devices.Application.Exceptions;
using Devices.Application.UseCases.Devices;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.UnitTests.Fakes;

namespace Devices.UnitTests.UseCases
{
    public class DeleteDeviceUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_WhenNotFound_ThrowsNotFoundException()
        {
            var repo = new FakeDeviceRepository();
            var useCase = new DeleteDeviceUseCase(repo);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                useCase.ExecuteAsync(Guid.NewGuid(), CancellationToken.None));
        }

        [Fact]
        public async Task ExecuteAsync_WhenExists_RemovesDevice()
        {
            var repo = new FakeDeviceRepository();
            var device = new Device("ToDelete", "B", DeviceState.Available);
            repo.Seed(device);

            var useCase = new DeleteDeviceUseCase(repo);

            await useCase.ExecuteAsync(device.Id, CancellationToken.None);

            // Verify deletion using GetDeviceByIdUseCase (still application layer)
            var getUseCase = new GetDeviceByIdUseCase(repo);
            await Assert.ThrowsAsync<NotFoundException>(() =>
                getUseCase.ExecuteAsync(device.Id, CancellationToken.None));
        }
    }
}