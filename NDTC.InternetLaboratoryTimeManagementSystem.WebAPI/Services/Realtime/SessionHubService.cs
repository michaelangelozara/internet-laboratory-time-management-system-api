using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Services.Realtime
{
    internal class SessionHubService(
        IHubContext<SessionHub, ISessionHubClient> hubContext,
        IUserContext userContext)
        : ISessionHubService
    {
        public async Task PublishLoggedOutSessionOf()
        {
            await hubContext.Clients.All.LoggedOutSession(userContext.UserId);
        }

        public async Task PublishNewSessionOf(string schoolId, TimeSpan availableDuration)
        {
            await hubContext.Clients.All.NewSession(schoolId, availableDuration);
        }
    }
}
