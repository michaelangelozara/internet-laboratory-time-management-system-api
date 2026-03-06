namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts
{
    public sealed record AccountResponseDTO
    {
        public Guid Id { get; private init; }

        public Guid UserId { get; private init; }

        public string SchoolId { get; private init; }

        public bool Active { get; private init; }

        public TimeSpan Duration { get; private init; }

        public AccountResponseDTO(
            Guid id,
            Guid userId,
            string schoolId,
            bool active,
            long availableDuration,
            DateTime lastLoginAt)
        {
            Id = id;
            UserId = userId;
            SchoolId = schoolId;
            Active = active;

            if (active)
            {
                Duration = new(availableDuration - (DateTime.UtcNow - lastLoginAt).Ticks);
            }
            else
            {
                Duration = new(availableDuration);
            }

        }
    }
}
