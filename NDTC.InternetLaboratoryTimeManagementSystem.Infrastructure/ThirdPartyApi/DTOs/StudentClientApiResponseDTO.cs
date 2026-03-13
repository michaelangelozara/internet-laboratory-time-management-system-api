using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi.DTOs
{
    public sealed record StudentClientApiResponseDTO
    {
        public bool Success { get; private init; }

        public string SchoolYear { get; private init; }

        public int Total { get; private init; }

        public IEnumerable<StudentResponseDTO> Data { get; private set; }
    }
}
