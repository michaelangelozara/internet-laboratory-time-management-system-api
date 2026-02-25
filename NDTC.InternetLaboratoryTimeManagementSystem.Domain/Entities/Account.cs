using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class Account : Entity
    {
        public string RFID { get; private set; } = string.Empty;

        public DateTime? LastLoginAt { get; private set; }

        public TimeSpan RemainingDuration => (TotalDuration - (DateTime.UtcNow - LastLoginAt)) ?? TimeSpan.Zero;
        
        public TimeSpan TotalDuration { get; private set; }

        public bool IsLoggedIn { get; private set; } = false;

        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        protected Account() { }

        public static Account Create(string rfid)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                RFID = rfid,
                TotalDuration = TimeSpan.FromHours(2),
                LastLoginAt = DateTime.UtcNow
            };

            return account;
        }

        public void MarkAsLoggedIn()
        {
            LastLoginAt = DateTime.UtcNow;
            IsLoggedIn = true;
        }

        public void LogOut()
        {
            IsLoggedIn = false;
        }

    }
}
