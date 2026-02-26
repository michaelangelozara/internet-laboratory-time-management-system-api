using Microsoft.EntityFrameworkCore;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Helpers
{
    public static class DuplicateUpdateHelper
    {
        public static bool SchoolIdDuplicate(DbUpdateException exception)
        {
            return exception.InnerException != null && exception.InnerException.Message.Contains("IX_users_school_id", StringComparison.Ordinal);
        }

        public static bool RFIDDuplicate(DbUpdateException exception)
        {
            return exception.InnerException != null && exception.InnerException.Message.Contains("IX_accounts_rfid", StringComparison.Ordinal);
        }
    }
}
