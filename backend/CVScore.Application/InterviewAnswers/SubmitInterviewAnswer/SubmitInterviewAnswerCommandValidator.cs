using FluentValidation;

namespace CVScore.Application.InterviewAnswers.SubmitInterviewAnswer;

public class SubmitInterviewAnswerCommandValidator : AbstractValidator<SubmitInterviewAnswerCommand>
{
    public SubmitInterviewAnswerCommandValidator()
    {
        RuleFor(x => x.InterviewQuestionId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.AudioUrl).MaximumLength(1000);
        RuleFor(x => x.DurationInSeconds).GreaterThanOrEqualTo(0).When(x => x.DurationInSeconds.HasValue);
    }
}
