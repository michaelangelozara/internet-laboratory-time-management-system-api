namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students
{
    public sealed record DetailedStudentResponseDTO
    {
        public string Student_UID { get; init; }

        public string FirstName { get; init; }

        public string MiddleName { get; init; }

        public string LastName { get; init; }

        public string NameSuffix { get; init; }

        public string BirthDate { get; init; }

        public string Gender { get; init; }

        public string ContactNumber { get; init; }

        public string RFIDNumber { get; init; }

        public string Enrollment_UID { get; init; }

        public string CourseCode { get; init; }

        public string SchoolYear { get; init; }

        public string Semester { get; init; }

        public string EnrollmentStatus { get; init; }

        public string FullName { get; init; }
    }
}
