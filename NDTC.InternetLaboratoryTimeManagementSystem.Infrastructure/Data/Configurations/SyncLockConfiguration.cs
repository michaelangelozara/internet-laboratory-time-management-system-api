using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class SyncLockConfiguration : IEntityTypeConfiguration<SyncLock>
    {
        public void Configure(EntityTypeBuilder<SyncLock> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("sync_locks");

            builder.Property(sl => sl.Name)
                .HasMaxLength(50)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(sl => sl.IsRunning)
                .HasColumnName("is_running")
                .IsRequired();

            builder.Property(sl => sl.LockedAt)
                .HasColumnName("locked_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(sl => sl.LockedBy)
                .HasColumnName("locked_by")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(sl => sl.Name).IsUnique();
        }
    }
}
