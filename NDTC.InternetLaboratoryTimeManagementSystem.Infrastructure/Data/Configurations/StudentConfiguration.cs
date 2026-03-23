using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ApplyEntityBase();

            builder.ToTable("students");

            builder.Property(s => s.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.NameSuffix)
                .HasColumnName("name_suffix")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(s => s.BirthDate)
                .HasColumnName("birth_date")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Gender)
                .HasColumnName("gender")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.ContactNumber)
                .HasColumnName("contact_number")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.EnrollmentUID)
                .HasColumnName("enrollment_uid")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.CourseCode)
                .HasColumnName("course_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.SchoolYear)
                .HasColumnName("school_year")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(s => s.Semester)
                .HasColumnName("semester")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(s => s.EnrollmentStatus)
                .HasColumnName("enrollment_status")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(s => s.SchoolId)
                .HasColumnName("school_id")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.UserId)
                .HasColumnName("user_id");

            builder.HasIndex(s => s.SchoolId)
                .IsUnique();

            builder.HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
