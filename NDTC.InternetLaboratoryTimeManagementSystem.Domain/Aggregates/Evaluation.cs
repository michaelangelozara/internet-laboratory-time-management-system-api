using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class Evaluation
        : Entity, IAuditable
    {
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime? LastModifiedAt { get; private set; }

        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        public ICollection<AnswerEvaluation> AnswerEvaluations { get; } = [];

        protected Evaluation()
        {
            
        }
    }
}
