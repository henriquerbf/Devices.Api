using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Devices.Infrastructure.Persistance.Configurations
{
    public sealed class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("devices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Brand)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.State)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<DeviceState>(v, ignoreCase: true))
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CreationTime)
                .IsRequired();

            builder.HasIndex(x => x.Brand);
            builder.HasIndex(x => x.State);
        }
    }
}
