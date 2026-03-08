using NDTC.InternetLaboratoryTimeManagementSystem.Application;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddWebAPI()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseScalarUI();
}

await app.UseMigration();

await app.UseLogoutAllAccounts();

await app.UseStopSyncingAndLocking();

await app.UseRemoveAllRegisteredDevices();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHubEndpoints();

app.MapEndpoints();

app.Run();
