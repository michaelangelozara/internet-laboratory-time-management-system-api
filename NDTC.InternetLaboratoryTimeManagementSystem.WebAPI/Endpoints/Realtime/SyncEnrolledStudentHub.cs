using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime
{
    [Authorize]
    public class SyncEnrolledStudentHub : Hub<ISyncEnrolledStudentHubClient>
    {
    }
}
