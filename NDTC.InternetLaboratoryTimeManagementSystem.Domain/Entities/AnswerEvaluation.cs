using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities
{
    public sealed class AnswerEvaluation
        : Entity, IAuditable
    {
        public string Comment { get; private set; } = string.Empty;

        public EvaluationType Type { get; private set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime? LastModifiedAt { get; private set; }

        public Guid EvaluationId { get; private set; }

        public Evaluation? Evaluation { get; private set; }

        // answered by
        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        protected AnswerEvaluation()
        {
            
        }

        public static AnswerEvaluation Create(string comment, EvaluationType type, Guid userId)
        {
            var answerEvaluation = new AnswerEvaluation
            {
                Id = Guid.NewGuid(),
                Comment = comment,
                Type = type,
                UserId = userId
            };

            return answerEvaluation;
        }
    }
}
