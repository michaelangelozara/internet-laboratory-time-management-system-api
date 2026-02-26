using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.UpdateRFID
{
    public sealed record UpdateRFIDCommand(string NewRFID, string SchoolId) : IRequest<Result>;
}
