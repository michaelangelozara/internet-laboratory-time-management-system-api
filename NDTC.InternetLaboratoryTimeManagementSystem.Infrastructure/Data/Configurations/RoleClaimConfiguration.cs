using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("role_claims");

            builder.Property(rc => rc.Type)
                .HasColumnName("type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(rc => rc.Value)
                .HasColumnName("value")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(rc => rc.RoleId)
                .HasColumnName("role_id");

            builder.HasOne(rc => rc.Role)
                .WithMany(r => r.RoleClaims)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
