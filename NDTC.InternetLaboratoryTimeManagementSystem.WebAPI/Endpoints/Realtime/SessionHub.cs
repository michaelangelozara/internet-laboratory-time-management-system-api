using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime
{
    [Authorize]
    public class SessionHub(ISessionHubService sessionHubService)
        : Hub<ISessionHubClient>
    {
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await sessionHubService.PublishLoggedOutSessionOf();

            await base.OnDisconnectedAsync(exception);
        }
    }
}
