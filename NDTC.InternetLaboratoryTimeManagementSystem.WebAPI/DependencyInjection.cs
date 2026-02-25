using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;
using System.Reflection;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            // add all endpoints from this assembly
            services.AddEndpoints(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
