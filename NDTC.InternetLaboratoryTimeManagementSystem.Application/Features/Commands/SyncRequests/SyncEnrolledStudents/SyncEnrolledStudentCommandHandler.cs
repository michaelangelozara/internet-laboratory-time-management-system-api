using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.SyncRequests;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.SyncRequests;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.SyncRequests.SyncEnrolledStudents
{
    internal class SyncEnrolledStudentCommandHandler(
        ISyncRequestRepository syncRequestRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<SyncEnrolledStudentCommand, Result>
    {
        public async Task<Result> Handle(SyncEnrolledStudentCommand request, CancellationToken cancellationToken)
        {
            var studentSyncRequest = await syncRequestRepository.FindByNameAsync(SyncNames.StudentSync);
            if (studentSyncRequest.Status == SyncRequestStatus.Running)
                return Result.Failure(Error.Problem(
                    "StudentSync.InvalidState",
                    $"Student sync can only be executed when the request is in '{SyncRequestStatus.Completed}' state. " +
                    $"Current state: '{studentSyncRequest.Status}'."));

            studentSyncRequest.SetStatus(SyncRequestStatus.Pending);
            studentSyncRequest.MarkAsRequested();

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
