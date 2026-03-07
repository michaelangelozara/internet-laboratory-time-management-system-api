using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Services.Realtime
{
    public class SyncEnrolledStudentHubService(
        IHubContext<SyncEnrolledStudentHub, ISyncEnrolledStudentHubClient> hubContext)
        : ISyncEnrolledStudentHubService
    {
        public async Task PublishEnrolledStudentSyncingProgress(string processedPercentage)
        {
            await hubContext.Clients.All.SyncingProgress(processedPercentage);
        }
    }
}
