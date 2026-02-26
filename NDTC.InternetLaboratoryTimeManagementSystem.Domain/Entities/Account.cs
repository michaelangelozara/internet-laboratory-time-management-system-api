using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class Account : Entity
    {
        public string RFID { get; private set; } = string.Empty;

        public DateTime? LastLoginAt { get; private set; }

        [NotMapped]
        private TimeSpan RemainingDuration => (AvailableDuration - (DateTime.UtcNow - LastLoginAt)) ?? TimeSpan.Zero;
        
        public TimeSpan AvailableDuration { get; private set; }

        public bool IsLoggedIn { get; private set; } = false;

        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        public ICollection<SessionHistory> SessionHistories { get; } = [];

        protected Account() { }

        public static Account Create(string rfid)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                RFID = rfid,
                AvailableDuration = TimeSpan.FromHours(2),
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
            if (!IsLoggedIn)
                return;

            // persist a new session history
            // adding session history must only work if the account is logged in
            TimeSpan consumedTime = AvailableDuration - RemainingDuration;
            var sessionHistory = SessionHistory.Create(consumedTime);
            SessionHistories.Add(sessionHistory);

            // re-set the total duration
            AvailableDuration = RemainingDuration;

            IsLoggedIn = false;
        }
    }
}
