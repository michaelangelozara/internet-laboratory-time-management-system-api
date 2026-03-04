using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class SyncLock : Entity
    {
        public string Name { get; private set; } = string.Empty;

        public bool IsRunning { get; private set; } = false;

        public DateTime LockedAt { get; private set; }

        public string LockedBy { get; private set; } = string.Empty;

        protected SyncLock()
        {
            
        }

        public static SyncLock Create(string name)
        {
            var syncLock = new SyncLock
            {
                Id = Guid.NewGuid(),
                Name = name,
                LockedAt = DateTime.UtcNow
            };

            return syncLock;
        }

        public void SetLockedBy(string lockedBy)
        {
            LockedBy = lockedBy;
            LockedAt = DateTime.UtcNow;
        }

        public void MarkAsRunning()
        {
            IsRunning = true;
        }

        public void MarkAsNotRunning()
        {
            IsRunning = false;
        }
    }
}
