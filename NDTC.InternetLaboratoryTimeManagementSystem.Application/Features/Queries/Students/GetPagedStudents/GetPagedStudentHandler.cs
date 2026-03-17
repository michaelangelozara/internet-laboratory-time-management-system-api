using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Students.GetPagedStudents
{
    internal class GetPagedStudentHandler(IStudentRepository studentRepository)
        : IRequestHandler<GetPagedStudent, Result<PagedResult<BasicStudentResponseDTO>>>
    {
        public async Task<Result<PagedResult<BasicStudentResponseDTO>>> Handle(GetPagedStudent request, CancellationToken cancellationToken)
        {
            return Result.Success(await studentRepository.GetPagedAsync(request.PageNumber, request.PageSize, request.Query));
        }
    }
}
