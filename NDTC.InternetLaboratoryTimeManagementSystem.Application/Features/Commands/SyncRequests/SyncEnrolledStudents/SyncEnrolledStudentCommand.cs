using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.SyncRequests.SyncEnrolledStudents
{
    public sealed record SyncEnrolledStudentCommand() : IRequest<Result>;
}
