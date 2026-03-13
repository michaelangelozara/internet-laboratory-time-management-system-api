namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students
{
    public sealed record BasicStudentResponseDTO(
            Guid Id,
            string SchoolId,
            string FirstName,
            string MiddleName,
            string LastName,
            string CourseCode,
            string SchoolYear,
            string EnrollmentStatus)
    {
        public Guid Id { get; } = Id;

        public string SchoolId { get; } = SchoolId;

        public string Name { get; } = string.IsNullOrWhiteSpace(MiddleName) 
            ? $"{LastName}, {FirstName}" 
            : $"{LastName}, {FirstName} {MiddleName}";

        public string CourseCode { get; } = CourseCode;

        public string SchoolYear { get; } = SchoolYear;

        public string EnrollmentStatus { get; } = EnrollmentStatus;
    }
}
