using CVScore.Domain.Enums;

namespace CVScore.API.Contracts.InterviewProfiles;

public sealed class CreateInterviewProfileRequest
{
    public Guid UserId { get; set; }
    public Guid CvDocumentId { get; set; }
    public string TargetRole { get; set; } = string.Empty;
    public InterviewLevel TargetLevel { get; set; }
    public InterviewLanguage Language { get; set; } = InterviewLanguage.English;
    public string? CompanyName { get; set; }
    public string? JobDescription { get; set; }
    public string? TechStack { get; set; }
    public string? FocusAreas { get; set; }
    public string? Notes { get; set; }
}
