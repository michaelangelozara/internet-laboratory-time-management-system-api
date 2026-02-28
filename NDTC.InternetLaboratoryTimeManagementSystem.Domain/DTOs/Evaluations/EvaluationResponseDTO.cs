namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations
{
    public sealed record EvaluationResponseDTO(
        Guid Id,
        string Question,
        float LikedPercentage,
        float DislikedPercentage,
        int TotalAnswers,
        DateTime CreatedAt,
        DateTime? LastModifiedAt);
}
