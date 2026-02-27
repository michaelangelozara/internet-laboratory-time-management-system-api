using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using Quartz;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.BackgroundJobs
{
    internal class DurationTerminationProcessor(IAccountService accountService) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await accountService.TerminateInvalidSessions();
        }
    }
}
