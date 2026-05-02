using FluentValidation;

namespace CVScore.Application.InterviewSessions.GenerateInterviewQuestions;

public class GenerateInterviewQuestionsQueryValidator : AbstractValidator<GenerateInterviewQuestionsQuery>
{
    public GenerateInterviewQuestionsQueryValidator()
    {
        RuleFor(x => x.InterviewProfileId).NotEmpty();
        RuleFor(x => x.QuestionCount).InclusiveBetween(1, 10);
    }
}
