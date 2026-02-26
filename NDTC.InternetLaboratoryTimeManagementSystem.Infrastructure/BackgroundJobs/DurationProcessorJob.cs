using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using Quartz;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.BackgroundJobs
{
    internal class DurationProcessorJob(IAccountService accountService)
        : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await accountService.ResetAllAccountDurations();
        }
    }
}
