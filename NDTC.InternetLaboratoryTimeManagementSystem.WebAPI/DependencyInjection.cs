using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;
using System.Reflection;
using System.Text.Json;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            // add all endpoints from this assembly
            services.AddEndpoints(Assembly.GetExecutingAssembly());

            // make all json property naming convention to snake case
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
            });

            return services;
        }
    }
}
