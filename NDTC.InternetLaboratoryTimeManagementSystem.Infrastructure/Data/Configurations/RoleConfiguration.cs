using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("roles");

            builder.Property(r => r.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
