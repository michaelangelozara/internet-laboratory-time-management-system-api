using Microsoft.Extensions.Configuration;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi.DTOs;
using System.Net.Http.Json;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi
{
    public sealed class StudentServiceClientApi(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        public async Task<StudentClientApiResponseDTO?> GetEnrolledStudentsAsync()
        {
            string apiKey = configuration["NDTC:ApiKey"] 
                ?? throw new InvalidOperationException("Api key is not configured.");

            try
            {
                return await httpClient.GetFromJsonAsync<StudentClientApiResponseDTO>($"/ndtc-rfid-api/public/api/students?api_key={apiKey}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
