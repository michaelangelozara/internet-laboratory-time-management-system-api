namespace NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
