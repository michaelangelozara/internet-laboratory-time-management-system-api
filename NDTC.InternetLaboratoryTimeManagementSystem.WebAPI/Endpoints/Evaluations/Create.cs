using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Evaluations.Create;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Evaluations
{
    public class Create : IEndpoint
    {
        public sealed record CreateEvaluationRequest(string Question);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/evaluations", async (
                ISender sender,
                CreateEvaluationRequest request) =>
            {
                var command = new CreateEvaluationCommand(request.Question);
                var result = await sender.Send(command);

                return result.Match((id) => Results.Created($"/api/v1/evaluations/{id}", id), CustomResults.Problem);
            })
                .HasPermission(Permissions.Evaluation.Create)
                .WithTags(Tags.Evaluations)
                .WithDescription("This is used to create an evaluation.")
                .Produces(StatusCodes.Status201Created);
        }
    }
}
