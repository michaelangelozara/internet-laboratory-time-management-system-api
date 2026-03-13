namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students
{
    public sealed record StudentResponseDTO
    {
        public string Student_UID { get; private init; }

        public string FirstName { get; private init; }

        public string MiddleName { get; private init; }

        public string LastName { get; private init; }

        public string NameSuffix { get; private init; }

        public string BirthDate { get; private init; }

        public string Gender { get; private init; }

        public string ContactNumber { get; private init; }

        public string RFIDNumber { get; private init; }

        public string Enrollment_UID { get; private init; }

        public string CourseCode { get; private init; }

        public string SchoolYear { get; private init; }

        public string Semester { get; private init; }

        public string EnrollmentStatus { get; private init; }

        public string FullName { get; private init; }
    }
}
