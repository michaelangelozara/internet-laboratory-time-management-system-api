using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Logout
{
    internal class LogoutCommandHandler(
        IUserContext userContext,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        ISessionHubService sessionHubService)
        : IRequestHandler<LogoutCommand, Result>
    {
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var userId = userContext.UserId;

                var account = await accountRepository.FindByUserIdAsync(userId);
                if (account is null)
                    return Result.Failure(Error.NotFound("Invalid.User", "User is undefined."));

                account.LogOut();

                await unitOfWork.CommitAsync(cancellationToken);

                // publish to signalr
                await sessionHubService.PublishLoggedOutSessionOf();

                return Result.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
