namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface IStudentClientApiService
    {
        Task TryToSyncEnrolledStudents();
    }
}
