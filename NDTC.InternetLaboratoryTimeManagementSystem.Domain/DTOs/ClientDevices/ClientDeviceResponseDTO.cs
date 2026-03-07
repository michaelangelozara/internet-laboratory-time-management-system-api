namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices
{
    public sealed record ClientDeviceResponseDTO(
        Guid Id,
        string Name,
        DateTime ConnectedAt);
}
