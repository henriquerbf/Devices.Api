using Devices.Application.Contracts.Devices;
using Devices.Application.UseCases.Devices;
using Devices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers
{

    [ApiController]
    [Route("api/devices")]
    public sealed class DevicesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreateDeviceRequest request,
            [FromServices] CreateDeviceUseCase useCase,
            CancellationToken ct)
        {
            var created = await useCase.ExecuteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            [FromServices] GetDeviceByIdUseCase useCase,
            CancellationToken ct)
        {
            var device = await useCase.ExecuteAsync(id, ct);
            return Ok(device);
        }

        // GET /api/devices?brand=Apple OR /api/devices?state=Available OR /api/devices
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<DeviceResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromQuery] string? brand,
            [FromQuery] DeviceState? state,
            [FromServices] GetDevicesUseCase getAll,
            [FromServices] GetDevicesByBrandUseCase getByBrand,
            [FromServices] GetDevicesByStateUseCase getByState,
            CancellationToken ct)
        {
            if (!string.IsNullOrWhiteSpace(brand))
                return Ok(await getByBrand.ExecuteAsync(brand, ct));

            if (state is not null)
                return Ok(await getByState.ExecuteAsync(state.Value, ct));

            return Ok(await getAll.ExecuteAsync(ct));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateDeviceRequest request,
            [FromServices] UpdateDeviceUseCase useCase,
            CancellationToken ct)
        {
            var updated = await useCase.ExecuteAsync(id, request, ct);
            return Ok(updated);
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Patch(
            [FromRoute] Guid id,
            [FromBody] PatchDeviceRequest request,
            [FromServices] PatchDeviceUseCase useCase,
            CancellationToken ct)
        {
            var updated = await useCase.ExecuteAsync(id, request, ct);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteDeviceUseCase useCase,
            CancellationToken ct)
        {
            await useCase.ExecuteAsync(id, ct);
            return NoContent();
        }
    }
}
