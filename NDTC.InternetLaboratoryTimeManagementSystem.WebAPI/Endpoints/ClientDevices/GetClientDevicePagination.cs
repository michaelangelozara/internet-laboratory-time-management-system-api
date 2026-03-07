using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.ClientDevices.GetPagedClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.ClientDevices
{
    public class GetClientDevicePagination : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/client-devices", async (
                ISender sender,
                int page_number = 1,
                int page_size = 10) =>
            {
                var query = new GetPagedClientDeviceQuery(page_number, page_size);
                var result = await sender.Send(query);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .HasPermission(Permissions.PC.Read)
                .WithTags(Tags.ClientDevices)
                .Produces<PagedResult<ClientDeviceResponseDTO>>(StatusCodes.Status200OK)
                .WithDescription("This API endpoint is used to fetch a paginated list of currently active client devices or computers.");
        }
    }
}
