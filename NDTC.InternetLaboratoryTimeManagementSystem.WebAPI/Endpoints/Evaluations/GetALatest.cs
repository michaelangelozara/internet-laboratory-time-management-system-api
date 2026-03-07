using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Evaluations.GetLatestEvaluation;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Evaluations
{
    public class GetALatest : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/evaluations/new", async (
                ISender sender) =>
            {
                var result = await sender.Send(new GetLatestEvaluationCommand());

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .WithTags(Tags.Evaluations)
                .WithDescription("This endpoint is used to fetch the full details of the latest evaluation.")
                .Produces<EvaluationResponseDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
        }
    }
}
