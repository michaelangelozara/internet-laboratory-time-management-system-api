using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Students.Create;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Students
{
    public class Create : IEndpoint
    {

        public sealed record CreateStudentRequest(string SchoolId, string RFID);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/students", async (
                ISender sender,
                CreateStudentRequest request) =>
            {
                var command = new CreateStudentCommand(request.SchoolId, request.RFID);
                var result = await sender.Send(command);

                return result.Match((id) => Results.Created($"/api/v1/users/students/{id}", id), CustomResults.Problem);
            })
                .HasPermission(Permissions.User.Create)
                .WithTags(Tags.Users);
        }
    }
}
