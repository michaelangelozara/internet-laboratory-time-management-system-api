using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime
{
    [Authorize]
    public class SessionHub
        : Hub<ISessionHubClient>
    {
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
