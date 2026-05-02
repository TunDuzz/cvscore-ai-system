using FluentValidation;

namespace CVScore.Application.InterviewSessions.CompleteInterviewSession;

public class CompleteInterviewSessionCommandValidator : AbstractValidator<CompleteInterviewSessionCommand>
{
    public CompleteInterviewSessionCommandValidator()
    {
        RuleFor(x => x.InterviewSessionId).NotEmpty();
        RuleFor(x => x.OverallScore)
            .InclusiveBetween(0, 100)
            .When(x => x.OverallScore.HasValue);
        RuleFor(x => x.Summary).MaximumLength(4000);
    }
}
