using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class ClientDeviceConfiguration : IEntityTypeConfiguration<ClientDevice>
    {
        public void Configure(EntityTypeBuilder<ClientDevice> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("client_devices");

            builder.Property(cd => cd.Name)
                .HasColumnName("name")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(cd => cd.ConnectionId)
                .HasColumnName("connection_id")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(cd => cd.ConnectedAt)
                .HasColumnName("connected_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.HasIndex(cd => cd.Name).IsUnique();
            builder.HasIndex(cd => cd.ConnectionId).IsUnique();
        }
    }
}
