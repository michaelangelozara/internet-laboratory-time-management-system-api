using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Admins.Create
{
    public sealed record CreateAdminCommand(string SchoolId, string RFID) : IRequest<Result<Guid>>;
}
