using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.UpdateDuration
{
    internal class UpdateDurationCommandHandler(
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        ISessionHubService sessionHubService)
        : IRequestHandler<UpdateDurationCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateDurationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var account = await accountRepository.FindByUserIdAsync(request.UserId);
                if (account is null)
                    return Result.Failure<Guid>(Error.NotFound("Account.Invalid", "Account not found."));

                account.SetNewAvailableDuration(request.NewDuration);

                await unitOfWork.CommitAsync(cancellationToken);

                // publish new duration
                await sessionHubService.PublishUpdatedSession(account.UserId, account.AvailableDurationTimeSpan);

                return Result.Success(account.Id);
            }
            catch (ApplicationException)
            {
                return Result.Failure<Guid>(Error.Problem("DurationFormat.Invalid", $"Invalid duration. {request.NewDuration} is invalid."));
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
