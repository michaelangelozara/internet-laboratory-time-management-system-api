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
                .WithDescription("Returns access token with optional duration property. If the role is admin then the duration will be null. If the role is student the duration will have a value")
                .Produces<AuthenticationResponseDTO>(StatusCodes.Status200OK)
                .Produces<Error>(StatusCodes.Status401Unauthorized);
        }
    }
}
