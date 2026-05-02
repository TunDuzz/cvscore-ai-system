using FluentValidation;

namespace CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;

public class EvaluateInterviewAnswerCommandValidator : AbstractValidator<EvaluateInterviewAnswerCommand>
{
    public EvaluateInterviewAnswerCommandValidator()
    {
        RuleFor(x => x.InterviewAnswerId).NotEmpty();
    }
}
