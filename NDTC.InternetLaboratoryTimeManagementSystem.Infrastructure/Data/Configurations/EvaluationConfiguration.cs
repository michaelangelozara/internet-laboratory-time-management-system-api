using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.ApplyEntityBase();

            builder.ApplyAuditing();

            builder.ToTable("evaluations");

            builder.Property(e => e.UserId)
                .HasColumnName("user_id");

            builder.Property(e => e.Question)
                .HasColumnName("question")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .HasDefaultValue(true);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Evaluations)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
