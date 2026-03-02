using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Realtime.RestartPC
{
    internal class RestartPCByUserIdCommandHandler(
        ISessionHubService sessionHubService,
        IAccountRepository accountRepository)
        : IRequestHandler<RestartPCByUserIdCommand, Result>
    {
        public async Task<Result> Handle(RestartPCByUserIdCommand request, CancellationToken cancellationToken)
        {
            if (!await accountRepository.DoesExistByUserIdAsync(request.UserId))
                throw new ApplicationException($"User with an id of {request.UserId} is not registered."); 

            if (await accountRepository.IsSignedInByUserIdAsync(request.UserId))
                return Result.Failure(Error.Problem("RestartingPC.Failed", "User is currently signed in."));

            await sessionHubService.PublishRestartSignalTo(request.UserId);

            return Result.Success();
        }
    }
}
