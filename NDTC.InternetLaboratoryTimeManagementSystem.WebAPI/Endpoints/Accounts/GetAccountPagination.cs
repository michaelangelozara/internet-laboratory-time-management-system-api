using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts.GetPagedAccounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Accounts
{
    public class GetAccountPagination : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/accounts", async (
                ISender sender,
                int page_number = 1,
                int page_size = 10,
                bool? active = null) =>
            {
                var query = new GetPagedAccountQuery(page_number, page_size, active);
                var result = await sender.Send(query);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .HasPermission(Permissions.Account.Read)
                .WithTags(Tags.Accounts)
                .Produces<PagedResult<AccountResponseDTO>>(StatusCodes.Status200OK)
                .WithDescription("This is used to fetch the student accounts pagination. The active is optional, if it is null, this returns all active and inactive accounts. If it is true returns active accounts only and vice versa.");
        }
    }
}
