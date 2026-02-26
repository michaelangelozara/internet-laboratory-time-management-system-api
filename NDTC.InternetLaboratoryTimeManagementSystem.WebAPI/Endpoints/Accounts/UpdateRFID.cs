using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.UpdateRFID;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Accounts
{
    public class UpdateRFID : IEndpoint
    {
        public sealed record UpdateRFIDRequest(string NewRFID, string SchoolId);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/accounts/rfid", async (
                ISender sender,
                UpdateRFIDRequest request) =>
            {
                var command = new UpdateRFIDCommand(request.NewRFID, request.SchoolId);
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .HasPermission(Permissions.Account.UpdateRFID)
                .WithTags(Tags.Accounts);
        }
    }
}
