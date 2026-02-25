using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        string Create(User user);
    }
}
