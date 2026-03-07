using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Realtime.ClientDevices.RestartPC;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.ClientDevices
{
    public class RestartSignal : IEndpoint
    {

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/client-devices/restart", async (
                ISender sender,
                string device_name) =>
            {
                var command = new RestartPCByNameCommand(device_name);
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .HasPermission(Permissions.PC.Restart)
                .WithTags(Tags.ClientDevices)
                .WithDescription("This endpoint can restart a client whether the user is currently signed in or signed out, as long as the client machine is connected to the system.")
                .Produces(StatusCodes.Status200OK);
        }
    }
}
