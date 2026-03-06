namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts
{
    public sealed record SessionHistoryResponseDTO
    {
        public string SchoolId { get; private init; }

        public TimeSpan ConsumedTime { get; private init; }

        public SessionHistoryResponseDTO(string schoolId, long consumedTime)
        {
            SchoolId = schoolId;
            ConsumedTime = new(consumedTime);
        }
    }
}
