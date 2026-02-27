using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Accounts.UpdateDuration
{
    public sealed record UpdateDurationCommand(Guid UserId, string NewDuration) : IRequest<Result<Guid>>;
}
