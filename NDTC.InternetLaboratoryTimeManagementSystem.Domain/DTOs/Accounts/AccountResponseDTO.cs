namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts
{
    public sealed record AccountResponseDTO(
        Guid Id,
        Guid UserId,
        bool Active,
        TimeSpan Duration);
}
