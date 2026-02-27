using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates
{
    public sealed class Evaluation
        : Entity, IAuditable
    {
        public string Question { get; private set; } = string.Empty;

        public bool Active { get; private set; } = true;
        
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime? LastModifiedAt { get; private set; }

        // created by
        public Guid UserId { get; private set; }

        public User? User { get; private set; }

        public ICollection<AnswerEvaluation> AnswerEvaluations { get; } = [];

        protected Evaluation()
        {
            
        }

        public static Evaluation Create(string question, Guid createBy)
        {
            var evaluation = new Evaluation
            {
                Id = Guid.NewGuid(),
                Question = question,
                UserId = createBy
            };

            return evaluation;
        }

        public void SetQuestion(string question)
        {
            Question = question;
        }

        public Result AddAnswerEvaluation(string comment, string evaluationType, Guid userId)
        {
            if (!Enum.TryParse<EvaluationType>(evaluationType, out var t))
                return Result.Failure(Error.Problem("EvaluationType.Invalid", $"{evaluationType} cannot be parsed as AnswerEvaluation's type."));

            var answerEvaluation = AnswerEvaluation.Create(comment, t, userId);
            AnswerEvaluations.Add(answerEvaluation);
            return Result.Success();
        }
    }
}
