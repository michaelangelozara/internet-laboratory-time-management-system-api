using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.DTOs.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.Authenticate
{
    public sealed record AuthenticationCommand(string RFID) : IRequest<Result<AuthenticationResponseDTO>>;
}
