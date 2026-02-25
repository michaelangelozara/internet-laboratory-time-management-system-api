
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Configurations
{
    public class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity, IAuditable
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
            .IsRequired();
            builder.HasIndex(e => e.CreatedAt);

            builder.Property(e => e.LastModifiedAt)
                .HasColumnName("last_modified_at")
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);
            builder.HasIndex(e => e.LastModifiedAt);

        }
    }
}
