using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Evaluations.GetPagedEvaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Evaluations
{
    public class GetPagination : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/evaluations", async (
                ISender sender,
                int page_number = 1,
                int page_size = 10) =>
            {
                var query = new GetPagedEvaluationQuery(page_number, page_size);
                var result = await sender.Send(query);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .HasPermission(Permissions.Evaluation.Read)
                .WithTags(Tags.Evaluations)
                .WithDescription("Evaluation's pagination. This can only be used by the super admin and admin roles.")
                .Produces<PagedResult<EvaluationResponseDTO>>(StatusCodes.Status200OK);
        }
    }
}
