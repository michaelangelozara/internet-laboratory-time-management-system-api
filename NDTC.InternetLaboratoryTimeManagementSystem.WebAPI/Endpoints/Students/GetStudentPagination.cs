using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Students.GetPagedStudents;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Infrastructure;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Students
{
    public class GetStudentPagination : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/students", async (
                ISender sender,
                int page_number = 1,
                int page_size = 10) =>
            {
                var query = new GetPagedStudent(page_number, page_size);
                var result = await sender.Send(query);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .HasPermission(Permissions.User.Read)
                .WithTags(Tags.Users)
                .Produces<PagedResult<BasicStudentResponseDTO>>(StatusCodes.Status200OK)
                .WithDescription("Returns a paginated collection of students.");
        }
    }
}
