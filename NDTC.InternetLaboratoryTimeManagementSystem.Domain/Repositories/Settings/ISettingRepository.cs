using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Settings
{
    public interface ISettingRepository
    {
        Task<Setting> Find();
    }
}
