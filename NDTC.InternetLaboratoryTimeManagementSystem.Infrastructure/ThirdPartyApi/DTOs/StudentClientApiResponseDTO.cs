using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi.DTOs
{
    public sealed record StudentClientApiResponseDTO
    {
        public bool Success { get; init; }

        public string SchoolYear { get; init; }

        public int Total { get; init; }

        public IEnumerable<DetailedStudentResponseDTO> Data { get; set; }
    }
}
