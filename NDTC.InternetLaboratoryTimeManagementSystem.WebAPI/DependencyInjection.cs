using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime;
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

            // make all json property naming convention to snake case
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
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
