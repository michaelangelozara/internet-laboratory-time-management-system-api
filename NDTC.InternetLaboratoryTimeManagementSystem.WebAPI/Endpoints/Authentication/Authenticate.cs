using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.DTOs.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Authenticate;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Authentication
{
    public class Authenticate : IEndpoint
    {

        public sealed record AuthenticationRequest(string RFID);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/accounts/authenticate", async (
                AuthenticationRequest request,
                ISender sender) =>
            {
                var command = new AuthenticationCommand(request.RFID);
                var result = await sender.Send(command);

                return result.Match((data) => Results.Ok(data), CustomResults.Problem);
            })
                .AllowAnonymous()
                .WithTags(Tags.Accounts)
                .WithDescription("Returns an access token with an optional duration property. If the authenticated user's role is admin, the duration value will be null. If the role is student, the duration property will contain a value indicating the account's validity duration.")
                .Produces<AuthenticationResponseDTO>(StatusCodes.Status200OK)
                .Produces<Error>(StatusCodes.Status401Unauthorized);
        }
    }
}
