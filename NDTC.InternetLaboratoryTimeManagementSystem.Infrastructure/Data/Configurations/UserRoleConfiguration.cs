using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Property(ur => ur.UserId)
                .HasColumnName("user_id");

            builder.Property(ur => ur.RoleId)
               .HasColumnName("role_id");

            builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
