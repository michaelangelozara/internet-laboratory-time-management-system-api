using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Logout
{
    internal class LogoutCommandHandler(
        IUserContext userContext,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<LogoutCommand, Result>
    {
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var account = await accountRepository.FindByUserIdAsync(userContext.UserId);
                if (account is null)
                    return Result.Failure(Error.NotFound("Invalid.User", "User is undefined."));

                account.LogOut();

                await unitOfWork.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
