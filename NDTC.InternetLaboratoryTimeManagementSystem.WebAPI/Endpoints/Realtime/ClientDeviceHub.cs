using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime
{
    [AllowAnonymous]
    public class ClientDeviceHub(IClientDeviceService clientDeviceService)
        : Hub<IClientDeviceHubClient>
    {
        public async Task RegisterDevice(string name)
        {
            string connectionId = Context.ConnectionId;
            await clientDeviceService.RegisterDevice(name, connectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            await clientDeviceService.UnregisterDevice(connectionId);
        }
    }
}
