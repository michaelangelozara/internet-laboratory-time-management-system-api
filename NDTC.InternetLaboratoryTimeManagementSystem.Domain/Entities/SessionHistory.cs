using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class SessionHistory 
        : Entity
    {
        public long ConsumedTime { get; private set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public Guid AccountId { get; private set; }

        public Account? Account { get; private set; }

        protected SessionHistory()
        {
            
        }

        public static SessionHistory Create(long consumedTime)
        {
            var sessionHistory = new SessionHistory
            {
                Id = Guid.NewGuid(),
                ConsumedTime = consumedTime
            };

            return sessionHistory;
        }
    }
}
