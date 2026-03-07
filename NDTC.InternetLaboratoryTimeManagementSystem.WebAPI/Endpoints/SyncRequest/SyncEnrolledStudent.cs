using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.SyncRequests.SyncEnrolledStudents;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.SyncRequest
{
    public class SyncEnrolledStudent : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/sync-requests/enrolled-students", async (
                ISender sender) =>
            {
                var command = new SyncEnrolledStudentCommand();
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .HasPermission(Permissions.SyncRequest.SyncStudentData)
                .WithTags(Tags.SyncRequests)
                .WithDescription("This endpoint is used to sync enrolled students and can only be accessed by users with superadmin or admin roles")
                .Produces(StatusCodes.Status200OK);
        }
    }
}
