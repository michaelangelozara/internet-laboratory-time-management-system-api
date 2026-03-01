using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Services.Realtime;
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

            services.ConfigureHttpJsonOptions(options =>
            {
                // make all json property naming convention to snake case
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;

                // add custom Date and Time format
                options.SerializerOptions.Converters.Add(new CustomDateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            });

            services.AddRealtimeServices();

            return services;
        }

        private static IServiceCollection AddRealtimeServices(this IServiceCollection services)
        {
            services.AddScoped<ISessionHubService, SessionHubService>();

            return services;
        }
    }
}
