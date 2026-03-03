using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ApplyEntityBase();

            builder.ApplyAuditing();

            builder.ToTable("settings");

            builder.Property(s => s.IsSyncing)
                .HasColumnName("is_syncing")
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
