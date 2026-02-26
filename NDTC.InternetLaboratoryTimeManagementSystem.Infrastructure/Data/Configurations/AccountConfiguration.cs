using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("accounts");

            builder.Property(a => a.RFID)
                .HasColumnName("rfid")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.UserId)
                .HasColumnName("user_id");

            builder.Property(a => a.LastLoginAt)
                .HasColumnName("last_login_at")
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);

            builder.Property(a => a.AvailableDuration)
                .HasColumnName("available_duration")
                .HasColumnType("interval")
                .IsRequired();

            builder.Property(a => a.IsLoggedIn)
                .HasColumnName("is_logged_in")
                .HasDefaultValue(false);

            builder.HasIndex(a => a.RFID)
                .IsUnique();

            builder.HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<Account>(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
