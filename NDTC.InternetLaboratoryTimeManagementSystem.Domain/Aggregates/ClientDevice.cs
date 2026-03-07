using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class ClientDevice : Entity
    {
        public string Name { get; private init; }

        public string ConnectionId { get; private init; }

        public DateTime ConnectedAt { get; } = DateTime.UtcNow;

        protected ClientDevice()
        {
            
        }

        public static ClientDevice Create(string name, string connectionId)
        {
            var clientDevice = new ClientDevice
            {
                Id = Guid.NewGuid(),
                Name = name,
                ConnectionId = connectionId
            };

            return clientDevice;
        }
    }
}
