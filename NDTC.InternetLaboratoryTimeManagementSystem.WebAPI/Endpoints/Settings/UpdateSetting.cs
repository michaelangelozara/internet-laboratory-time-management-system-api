using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Settings.Update;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Settings
{
    public class UpdateSetting : IEndpoint
    {
        public sealed record UpdateSettingRequest(bool IsSyncing);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/settings", async (
                ISender sender,
                UpdateSettingRequest request) =>
            {
                var command = new UpdateSettingCommand(request.IsSyncing);
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .HasPermission(Permissions.Setting.SyncStudentData)
                .WithTags(Tags.Settings)
                .WithDescription("This is used to perform syncing enrolled student. This can only be used with super admin and admin roles.")
                .Produces(StatusCodes.Status200OK);
        }
    }
}
