using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.AnswerEvaluations.Create;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.AnswerEvaluations
{
    public class Create : IEndpoint
    {
        public sealed record CreateAnswerEvaluationRequest(string Comment, string EvaluationType);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/evaluations/{id:guid}/answers", async (
                Guid id,
                ISender sender,
                CreateAnswerEvaluationRequest request) =>
            {
                var command = new CreateAnswerEvaluationCommand(id, request.Comment, request.EvaluationType);
                var result = await sender.Send(command);

                return result.Match((id) => Results.Created($"/api/v1/evaluations/answers/{id}", id), CustomResults.Problem);
            })
                .WithTags(Tags.Evaluations);
        }
    }
}
