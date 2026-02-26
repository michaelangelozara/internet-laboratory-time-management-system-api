using Quartz;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions
{
    internal static class QuartzConfigurationExtensions
    {
        public static void AddScheduledJob<TJob>(
            this IServiceCollectionQuartzConfigurator quartz,
            string jobName,
            string cronSchedule)
            where TJob : IJob
        {
            var jobKey = new JobKey(jobName);

            quartz.AddJob<TJob>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"{jobName}-trigger")
                .WithCronSchedule(cronSchedule));
        }
    }
}
