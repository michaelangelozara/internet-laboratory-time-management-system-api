using MediatR;
using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Admins.Create
{
    internal class CreateAdminCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IRoleManager roleManager) 
        : IRequestHandler<CreateAdminCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var user = User.Create(request.SchoolId, request.RFID);
                await userRepository.AddAsync(user);

                await roleManager.AddToRoleAsync(user, Roles.Admin);

                await unitOfWork.CommitAsync(cancellationToken);

                return Result.Success(user.Id);
            }
            catch (DbUpdateException ex) when (IsSchoolIdDuplicate(ex))
            {
                return Result.Failure<Guid>(Error.Problem("SchoolId.Invalid", "School id is existing."));
            }
            catch (DbUpdateException ex) when (IsRFIDDuplicate(ex))
            {
                return Result.Failure<Guid>(Error.Problem("RFID.Invalid", "RFID is existing."));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool IsSchoolIdDuplicate(DbUpdateException exception)
        {
            return exception.Message.Contains("IX_users_school_id", StringComparison.Ordinal);
        }

        private static bool IsRFIDDuplicate(DbUpdateException exception)
        {
            return exception.Message.Contains("IX_accounts_rfid", StringComparison.Ordinal);
        }
    }
}
