namespace NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; }
        DateTime? LastModifiedAt { get; }
    }
}
