using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts.GetPagedSessionHistories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Accounts
{
    public class GetSessionHistoryPagination : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/accounts/session-histories", async (
                ISender sender,
                int page_number = 1,
                int page_size = 10) =>
            {
                var query = new GetPagedSessionHistoryQuery(page_number, page_size);
                var result = await sender.Send(query);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .HasPermission(Permissions.Account.Read)
                .WithTags(Tags.Accounts)
                .Produces<PagedResult<SessionHistoryResponseDTO>>(StatusCodes.Status200OK)
                .WithDescription("This is used to fetch the student session history pagination.");
        }
    }
}
