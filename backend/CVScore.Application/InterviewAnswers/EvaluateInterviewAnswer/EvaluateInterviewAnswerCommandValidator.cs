using FluentValidation;

namespace CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;

public class EvaluateInterviewAnswerCommandValidator : AbstractValidator<EvaluateInterviewAnswerCommand>
{
    public EvaluateInterviewAnswerCommandValidator()
    {
        RuleFor(x => x.InterviewAnswerId).NotEmpty();
        RuleFor(x => x.Score).InclusiveBetween(0, 100);
        RuleFor(x => x.Strengths).MaximumLength(2000);
        RuleFor(x => x.Improvements).MaximumLength(2000);
        RuleFor(x => x.SuggestedAnswer).MaximumLength(4000);
        RuleFor(x => x.DetailedAnalysis).MaximumLength(5000);
    }
}
