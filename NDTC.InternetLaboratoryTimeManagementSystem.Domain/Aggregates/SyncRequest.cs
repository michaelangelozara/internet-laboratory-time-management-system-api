using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.SyncRequests;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class SyncRequest 
        : Entity
    {
        public string Name { get; private set; } = string.Empty;

        public DateTime RequestedAt { get; private set; }

        public DateTime CompletedAt { get; private set; }

        public SyncRequestStatus Status { get; private set; } = SyncRequestStatus.Completed;

        protected SyncRequest()
        {
            
        }

        public static SyncRequest Create(string name)
        {
            var now = DateTime.UtcNow;

            var setting = new SyncRequest
            {
                Id = Guid.NewGuid(),
                Name = name,
                RequestedAt = now,
                CompletedAt = now
            };

            return setting;
        }

        public void MarkAsRequested()
        {
            RequestedAt = DateTime.UtcNow;
        }

        public void MarkAsCompleted()
        {
            CompletedAt = DateTime.UtcNow;
        }

        public void SetStatus(SyncRequestStatus status)
        {
            Status = status;
        }
    }
}
