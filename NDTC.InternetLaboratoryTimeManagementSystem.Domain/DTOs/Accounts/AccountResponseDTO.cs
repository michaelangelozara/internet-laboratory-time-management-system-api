namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts
{
    public sealed record AccountResponseDTO(
        Guid Id,
        Guid UserId,
        string SchoolId,
        bool Active,
        TimeSpan Duration);
}
