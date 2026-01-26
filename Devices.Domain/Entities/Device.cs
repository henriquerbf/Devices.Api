using Devices.Domain.Enums;
using Devices.Domain.Exceptions;

namespace Devices.Domain.Entities
{
    /// <summary>
    /// Represents a Device.
    /// </summary>
    public sealed class Device
    {
        //Properties
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Brand { get; private set; }
        public DeviceState State { get; private set; }
        public DateTimeOffset CreationTime { get; private set; } // cannot be updated

        // Constructor required by EF Core (reflection)
        private Device() { }

        // Constructor used when creating new instances
        public Device(string name, string brand, DeviceState state)
        {
            Id = Guid.NewGuid();
            Name = NormalizeRequired(name, nameof(name));
            Brand = NormalizeRequired(brand, nameof(brand));
            State = state;
            CreationTime = DateTime.UtcNow;
        }

        // Guard Methods
        public void UpdateName(string newName)
        {
            EnsureNotInUseForPropertyUpdate();
            Name = NormalizeRequired(newName, nameof(newName));
        }

        public void UpdateBrand(string newBrand)
        {
            EnsureNotInUseForPropertyUpdate();
            Brand = NormalizeRequired(newBrand, nameof(newBrand));
        }

        public void ChangeState(DeviceState newState) => State = newState;

        public void EnsureDeletable()
        {
            if (State == DeviceState.InUse)
                throw new DeviceInUseException("In-use devices cannot be deleted.");
        }

        private void EnsureNotInUseForPropertyUpdate()
        {
            if (State == DeviceState.InUse)
                throw new DeviceInUseException("Name and brand cannot be updated when device is in use.");
        }

        private static string NormalizeRequired(string value, string fieldName)
        {
            var trimmed = (value ?? string.Empty).Trim();
            if (trimmed.Length == 0) throw new ArgumentException($"{fieldName} is required.", fieldName);
            return trimmed;
        }
    }
}
