using MediatR;
using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Helpers;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.UpdateRFID
{
    internal class UpdateRFIDCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateRFIDCommand, Result>
    {
        public async Task<Result> Handle(UpdateRFIDCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                var account = await accountRepository.FindBySchoolIdAsync(request.SchoolId);
                if (account is null)
                    return Result.Failure(Error.NotFound("Account.Invalid", "Account not found."));

                account.SetRFID(request.NewRFID);

                await unitOfWork.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch(DbUpdateException ex) when (DuplicateUpdateHelper.DuplicateRFID(ex))
            {
                return Result.Failure(Error.InvalidRFID());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
