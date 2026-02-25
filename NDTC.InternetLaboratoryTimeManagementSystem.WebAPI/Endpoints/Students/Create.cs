using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.WebAPI.Endpoints.Students
{
    public class Create : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/students/greet", async () =>
            {
                return Results.Ok("Hello, this is secured endpoint.");
            })
                .HasPermission(Permissions.User.Create)
                .WithTags(Tags.Users);
        }
    }
}
