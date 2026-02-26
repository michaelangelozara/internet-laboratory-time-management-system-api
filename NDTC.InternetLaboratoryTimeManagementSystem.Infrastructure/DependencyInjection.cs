using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Authorization;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.BackgroundJobs;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Seeds;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Database.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Options;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Time;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using Quartz;
using System.Security.Claims;
using System.Text;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddServices()
                .AddDatabase(configuration)
                .AddHealthChecks(configuration)
                .AddAuthenticationInternal(configuration)
                .AddAuthorizationInternal()
                .AddOptions(configuration)
                .AddRepositories()
                .AddBackgroundJobs();

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEvaluationRepository, EvaluationRepository>();
            services.AddScoped<IAnswerEvaluationRepository, AnswerEvaluationRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? dbConnection = configuration["DB"];
        if (dbConnection is null)
            throw new InvalidOperationException("Database connection string is not configured.");

            services.AddDbContext<AppDbContext>(options =>
            {
                options
                    .UseNpgsql(dbConnection)
                    .UseAsyncSeeding(async (context, _, cancellationToken) =>
                    {
                        await RoleAndPermissionSeeder.SeedAsync((AppDbContext)context, cancellationToken);

                        await SuperAdminSeeder.SeedAsync((AppDbContext)context, cancellationToken);
                    });
            });

            return services;
        }

        private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            //    .AddHealthChecks()
            //    .AddNpgSql(configuration.GetConnectionString("Database")!);

            return services;
        }

        private static IServiceCollection AddAuthenticationInternal(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>()
                ?? throw new InvalidOperationException("Jwt option is not configured.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false; // this should be disabled only in development environment
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            return services;
        }

        private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
        {
            services.AddAuthorization();

            // set all endpoints required authenticated user as default
            services.AddAuthorizationBuilder()
                .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

            services.AddScoped<PermissionProvider>();

            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }

        private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            return services;
        }

        private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                // runs every 10 minutes
                q.AddScheduledJob<DurationProcessorJob>(nameof(DurationProcessorJob), "0 0/10 * * * ?");
            });

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
            return services;
        }
    }
}
