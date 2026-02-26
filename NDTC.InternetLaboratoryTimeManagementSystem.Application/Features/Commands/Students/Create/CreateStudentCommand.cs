using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Students.Create
{
    public sealed record CreateStudentCommand(string SchoolId, string RFID) : IRequest<Result<Guid>>;
}
