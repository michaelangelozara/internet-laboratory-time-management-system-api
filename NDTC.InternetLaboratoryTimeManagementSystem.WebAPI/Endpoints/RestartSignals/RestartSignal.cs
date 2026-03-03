using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Realtime.RestartPC;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.ShutdownSignals
{
    public class RestartSignal : IEndpoint
    {
        public sealed record RestartSignalRequest(Guid UserId);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/pc/restart", async (
                ISender sender,
                RestartSignalRequest request) =>
            {
                var command = new RestartPCByUserIdCommand(request.UserId);
                var result = await sender.Send(command);

                return result.Match(() => Results.Ok(), CustomResults.Problem);
            })
                .HasPermission(Permissions.PC.Restart)
                .WithTags(Tags.PC)
                .WithDescription("This is used to send a restart signal to client in realtime using user id. Take note that currently signed in account cannot be restarted.")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
