using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Logout;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Authentication
{
    public class Logout : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/accounts/logout", async (
                ISender sender) =>
            {
                var command = new LogoutCommand();
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .WithTags(Tags.Accounts)
                .WithDescription("This endpoint requires an authenticated user. Ensure that the access token is included in the request before calling this endpoint.")
                .Produces(StatusCodes.Status200OK);
        }
    }
}
