using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Evaluations.Update;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Evaluations
{
    public class Update : IEndpoint
    {
        public sealed record UpdateEvaluationRequest(string Question);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/evaluations/{id:guid}", async (
                Guid id,
                ISender sender,
                UpdateEvaluationRequest request) =>
            {
                var command = new UpdateEvaluationCommand(id, request.Question);
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .HasPermission(Permissions.Evaluation.Update)
                .WithTags(Tags.Evaluations);
        }
    }
}
