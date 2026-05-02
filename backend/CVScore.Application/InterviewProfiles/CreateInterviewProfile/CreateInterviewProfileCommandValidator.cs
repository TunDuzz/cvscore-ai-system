using FluentValidation;

namespace CVScore.Application.InterviewProfiles.CreateInterviewProfile;

public class CreateInterviewProfileCommandValidator : AbstractValidator<CreateInterviewProfileCommand>
{
    public CreateInterviewProfileCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.CvDocumentId).NotEmpty();
        RuleFor(x => x.TargetRole).NotEmpty().MaximumLength(150);
        RuleFor(x => x.CompanyName).MaximumLength(150);
        RuleFor(x => x.JobDescription).MaximumLength(4000);
        RuleFor(x => x.TechStack).MaximumLength(1000);
        RuleFor(x => x.FocusAreas).MaximumLength(1000);
        RuleFor(x => x.Notes).MaximumLength(2000);
    }
}
