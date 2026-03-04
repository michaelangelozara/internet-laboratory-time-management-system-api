using Microsoft.AspNetCore.SignalR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Services.Realtime
{
    public class SyncEnrolledStudentService(
        IHubContext<SyncEnrolledStudentHub, ISyncEnrolledStudentHubClient> hubContext)
        : ISyncEnrolledStudentService
    {
        public async Task PublishEnrolledStudentSyncingProgress(string processedPercentage)
        {
            await hubContext.Clients.All.SyncingProgress(processedPercentage);
        }
    }
}
