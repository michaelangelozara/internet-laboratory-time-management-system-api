namespace NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants
{
    public static class Permissions
    {
        public static class User
        {
            public const string Create = "users:create";
            public const string Update = "users:update";
            public const string Delete = "users:delete";
            public const string Read = "users:read";
        }

        public static class Account
        {
            public const string UpdateRFID = "accounts:update_rfid";
        }
    }
}
