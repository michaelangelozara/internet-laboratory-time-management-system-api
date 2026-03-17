namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts
{
    public sealed record SessionHistoryResponseDTO
    {
        public string SchoolId { get; private init; }

        public string FullName { get; private init; }

        public TimeSpan ConsumedTime { get; private init; }

        public SessionHistoryResponseDTO(
            string schoolId, 
            string firstName,
            string? middleName,
            string lastName, 
            long consumedTime)
        {
            SchoolId = schoolId;
            ConsumedTime = new(consumedTime);
            FullName = string.IsNullOrWhiteSpace(middleName) ? $"{lastName}, {firstName}" : $"{lastName}, {firstName} {middleName}";
        }
    }
}
