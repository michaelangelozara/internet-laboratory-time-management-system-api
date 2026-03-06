using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts.GetPagedSessionHistories
{
    public sealed record GetPagedSessionHistoryQuery(int PageNumber, int PageSize)
        : IRequest<Result<PagedResult<SessionHistoryResponseDTO>>>;
}
