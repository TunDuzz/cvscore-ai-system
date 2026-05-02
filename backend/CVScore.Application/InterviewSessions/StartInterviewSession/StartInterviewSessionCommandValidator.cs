using FluentValidation;

namespace CVScore.Application.InterviewSessions.StartInterviewSession;

public class StartInterviewSessionCommandValidator : AbstractValidator<StartInterviewSessionCommand>
{
    public StartInterviewSessionCommandValidator()
    {
        RuleFor(x => x.InterviewProfileId).NotEmpty();
        RuleFor(x => x.Questions).NotEmpty();

        RuleForEach(x => x.Questions).ChildRules(question =>
        {
            question.RuleFor(x => x.DisplayOrder).GreaterThan(0);
            question.RuleFor(x => x.Content).NotEmpty().MaximumLength(2000);
            question.RuleFor(x => x.ExpectedAnswerPoints).MaximumLength(2000);
            question.RuleFor(x => x.AiRationale).MaximumLength(2000);
        });
    }
}
