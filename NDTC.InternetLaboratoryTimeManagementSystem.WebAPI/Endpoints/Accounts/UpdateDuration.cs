using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.UpdateDuration;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Accounts
{
    public class UpdateDuration : IEndpoint
    {
        public sealed record UpdateDurationRequest(Guid UserId, string NewDuration);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/accounts/time", async (
                ISender sender,
                UpdateDurationRequest request) =>
            {
                var command = new UpdateDurationCommand(request.UserId, request.NewDuration);
                var result = await sender.Send(command);

                return result.Match((id) => Results.Ok(id), CustomResults.Problem);
            })
                .HasPermission(Permissions.Account.UpdateDuration)
                .WithTags(Tags.Accounts)
                .WithDescription("This is used to update the time/duration of the particular user.")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
        }
    }
}
