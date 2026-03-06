using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class SessionHistoryConfiguration : IEntityTypeConfiguration<SessionHistory>
    {
        public void Configure(EntityTypeBuilder<SessionHistory> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("session_histories");

            builder.Property(sh => sh.ConsumedTime)
                .HasColumnName("consumed_time")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(sh => sh.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("now()")
                .IsRequired();

            builder.Property(sh => sh.AccountId)
                .HasColumnName("account_id");

            builder.HasOne(sh => sh.Account)
                .WithMany(a => a.SessionHistories)
                .HasForeignKey(sh => sh.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
