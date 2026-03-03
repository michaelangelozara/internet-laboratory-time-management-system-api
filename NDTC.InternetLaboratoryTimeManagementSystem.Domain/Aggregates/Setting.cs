using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class Setting 
        : Entity, IAuditable
    {
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime? LastModifiedAt { get; private set; }

        public bool IsSyncing { get; private set; } = false;

        protected Setting()
        {
            
        }

        public static Setting Create()
        {
            var setting = new Setting
            {
                Id = Guid.NewGuid()
            };

            return setting;
        }
    }
}
