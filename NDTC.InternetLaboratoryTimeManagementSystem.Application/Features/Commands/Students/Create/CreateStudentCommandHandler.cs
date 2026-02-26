using MediatR;
using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Helpers;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Students.Create
{
    internal class CreateStudentCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
        : IRequestHandler<CreateStudentCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var user = User.Create(request.SchoolId, request.RFID);
                await userRepository.AddAsync(user);

                await unitOfWork.CommitAsync(cancellationToken);

                return Result.Success(user.Id);
            }
            catch (DbUpdateException ex) when (DuplicateUpdateHelper.DuplicateSchoolId(ex))
            {
                return Result.Failure<Guid>(Error.InvalidSchoolId());
            }
            catch (DbUpdateException ex) when (DuplicateUpdateHelper.DuplicateRFID(ex))
            {
                return Result.Failure<Guid>(Error.InvalidRFID());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
