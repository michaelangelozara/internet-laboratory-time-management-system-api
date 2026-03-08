using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Realtime;
using Scalar.AspNetCore;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseScalarUI(this WebApplication app)
        {
            app.MapOpenApi().AllowAnonymous();
            app.MapScalarApiReference().AllowAnonymous();

            return app;
        }

        internal static async Task UseMigration(this WebApplication app)
        {
            // List of DbContexts that ensure to be existed
            using var scope = app.Services.CreateAsyncScope();
            using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await appDbContext.Database.MigrateAsync();
        }

        internal static async Task UseLogoutAllAccounts(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
            await accountService.LogoutAllAccounts();
        }

        internal static void MapHubEndpoints(this WebApplication app)
        {
            string hubs = "/api/v1/hubs";

            app.MapHub<SessionHub>($"{hubs}/session");
            app.MapHub<SyncEnrolledStudentHub>($"{hubs}/sync");
            app.MapHub<ClientDeviceHub>($"{hubs}/client-device");
        }
        
        internal static async Task UseStopSyncingAndLocking(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            var syncRequestService = scope.ServiceProvider.GetRequiredService<ISyncRequestService>();
            await syncRequestService.StopSyncingAsync();

            var syncLockService = scope.ServiceProvider.GetRequiredService<ISyncLockService>();
            await syncLockService.StopLockingAsync();
        }

        internal static async Task UseRemoveAllRegisteredDevices(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var clientDeviceService = scope.ServiceProvider.GetRequiredService<IClientDeviceService>();
            await clientDeviceService.RemoveAllRegisteredDevices();
        }
    }
}
