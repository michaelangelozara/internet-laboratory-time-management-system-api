using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Students.GetPagedStudents
{
    public sealed record GetPagedStudent(int PageNumber, int PageSize) 
        : IRequest<Result<PagedResult<BasicStudentResponseDTO>>>;
}
