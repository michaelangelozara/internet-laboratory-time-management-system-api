using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class Student : Entity
    {
        public string FirstName { get; private set; } = string.Empty;

        public string MiddleName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string NameSuffix { get; private set; } = string.Empty;

        public string BirthDate { get; private set; } = string.Empty;

        public string Gender { get; private set; } = string.Empty;

        public string ContactNumber { get; private set; } = string.Empty;

        public string EnrollmentUID { get; private set; } = string.Empty;

        public string CourseCode { get; private set; } = string.Empty;

        public string SchoolYear { get; private set; } = string.Empty;

        public string Semester { get; private set; } = string.Empty;

        public string EnrollmentStatus { get; private set; } = string.Empty;

        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        public static Student Create(
            string firstName,
            string middleName,
            string lastName,
            string nameSuffix,
            string birthDate,
            string gender,
            string contactNumber,
            string enrollmentUID,
            string courseCode,
            string schoolYear,
            string semester,
            string enrollmentStatus)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                NameSuffix = nameSuffix,
                BirthDate = birthDate,
                Gender = gender,
                ContactNumber = contactNumber,
                EnrollmentUID = enrollmentUID,
                CourseCode = courseCode,
                SchoolYear = schoolYear,
                Semester = semester,
                EnrollmentStatus = enrollmentStatus
            };

            return student;
        }
    }
}
