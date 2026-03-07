using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Admins.Create;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Admins
{
    public class Create : IEndpoint
    {
        public sealed record CreateAdminRequest(string SchoolId, string RFID);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/admins", async (
                CreateAdminRequest request,
                ISender sender) =>
            {
                var command = new CreateAdminCommand(request.SchoolId, request.RFID);
                var result = await sender.Send(command);

                return result.Match((id) => Results.Created($"/api/v1/users/admins/{id}", id), CustomResults.Problem);
            })
                .WithTags(Tags.Users)
                .HasPermission(Permissions.User.Create)
                .WithDescription("This endpoint is used to create a user with an admin role and can only be accessed by the superadmin or the system account.")
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status409Conflict);
        }
    }
}
