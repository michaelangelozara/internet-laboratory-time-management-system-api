namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface IStudentService
    {
        Task TryToSyncEnrolledStudents();
    }
}
