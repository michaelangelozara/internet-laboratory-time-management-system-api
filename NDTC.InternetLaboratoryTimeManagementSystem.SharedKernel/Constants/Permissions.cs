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
            public const string UpdateDuration = "accounts:update_duration";
            public const string Read = "accounts:read";
        }

        public static class Evaluation
        {
            public const string Create = "evaluations:create";
            public const string Update = "evaluations:update";
            public const string Delete = "evaluations:delete";
            public const string Read = "evaluations:read";
        }

        public static class PC
        {
            public const string Read = "pc:read";
            public const string Restart = "pc:restart";
        }

        public static class SyncRequest
        {
            public const string SyncStudentData = "setting:sync_student_data";
        }
    }
}
