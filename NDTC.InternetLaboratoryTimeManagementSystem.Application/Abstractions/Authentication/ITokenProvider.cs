using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        // Evaluation claim here is only use for student role
        // admin or super admin must set this to null
        string Create(User user, IList<string>? roles, bool? hasStudentAnsweredEvaluation = default);
    }
}
