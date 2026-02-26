using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;
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
            var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
            await accountRepository.SetIsLoggedInToFalse();
        }
    }
}
