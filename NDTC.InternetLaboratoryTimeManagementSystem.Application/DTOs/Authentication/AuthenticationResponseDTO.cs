namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.DTOs.Authentication
{
    public sealed record AuthenticationResponseDTO(string AccessToken, TimeSpan? Duration);
}
