using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Logout;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime
{
    [Authorize]
    public class SessionHub(ISender sender)
        : Hub<ISessionHubClient>
    {
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var command = new LogoutCommand();
            await sender.Send(command);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
