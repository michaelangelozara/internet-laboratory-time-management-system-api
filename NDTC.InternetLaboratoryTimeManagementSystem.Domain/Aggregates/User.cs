using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class User
        : Entity, IAuditable
    {
        public string SchoolId { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime? LastModifiedAt { get; private set; }

        public Account? Account { get; private set; }

        public Student? Student { get; private set; }

        public ICollection<UserRole> UserRoles { get; } = [];

        public ICollection<Evaluation> Evaluations { get; } = [];

        public ICollection<AnswerEvaluation> AnswerEvaluations { get; } = [];

        protected User() { }

        public static User Create(string schoolId, string rfid)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                Account = Account.Create(rfid)
            };

            return user;
        }

        public void SetStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);

            Student = student;
        }
    }
}
