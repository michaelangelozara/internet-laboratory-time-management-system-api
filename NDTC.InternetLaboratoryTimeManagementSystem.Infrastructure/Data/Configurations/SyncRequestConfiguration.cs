using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class SyncRequestConfiguration : IEntityTypeConfiguration<SyncRequest>
    {
        public void Configure(EntityTypeBuilder<SyncRequest> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("sync_requests");

            builder.Property(sr => sr.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(sr => sr.RequestedAt)
                .HasColumnName("requested_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(sr => sr.CompletedAt)
                .HasColumnName("completed_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(sr => sr.Status)
               .HasColumnName("status")
               .HasMaxLength(50)
               .HasConversion<string>()
               .IsRequired();

            builder.HasIndex(sr => sr.Name).IsUnique();
        }
    }
}
