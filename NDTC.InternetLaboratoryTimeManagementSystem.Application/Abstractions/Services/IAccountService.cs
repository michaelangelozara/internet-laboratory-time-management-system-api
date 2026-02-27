namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task LogoutAllAccounts();
        
        Task ResetAllAccountDurations();

        Task TerminateInvalidSessions();
    }
}
