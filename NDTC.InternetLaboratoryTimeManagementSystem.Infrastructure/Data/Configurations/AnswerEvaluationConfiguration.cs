using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class AnswerEvaluationConfiguration : IEntityTypeConfiguration<AnswerEvaluation>
    {
        public void Configure(EntityTypeBuilder<AnswerEvaluation> builder)
        {
            builder.ApplyEntityBase();

            builder.ApplyAuditing();

            builder.ToTable("answer_evaluations");

            builder.Property(ae => ae.Comment)
                .HasColumnName("comment")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(ae => ae.Type)
                .HasConversion<string>()
                .HasColumnName("type")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(ae => ae.UserId)
                .HasColumnName("user_id");

            builder.Property(ae => ae.EvaluationId)
                .HasColumnName("evaluation_id");

            builder.HasOne(ae => ae.Evaluation)
                .WithMany(e => e.AnswerEvaluations)
                .HasForeignKey(ae => ae.EvaluationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ae => ae.User)
               .WithMany(u => u.AnswerEvaluations)
               .HasForeignKey(ae => ae.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
