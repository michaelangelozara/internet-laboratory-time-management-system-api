using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Services.Realtime
{
    public class ClientDeviceHubService(IHubContext<ClientDeviceHub, IClientDeviceHubClient> hubContext)
        : IClientDeviceHubService
    {
        public async Task PublishRestartSignalTo(string connectionId)
        {
            await hubContext.Clients.Client(connectionId).Restart();
        }
    }
}
