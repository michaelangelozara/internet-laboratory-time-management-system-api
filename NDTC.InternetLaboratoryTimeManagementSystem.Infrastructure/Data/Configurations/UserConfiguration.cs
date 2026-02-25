using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ApplyEntityBase();

            builder.ApplyAuditing();

            builder.ToTable("users");

            builder.Property(u => u.SchoolId)
                .HasColumnName("school_id")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
