using CVScore.Domain.Enums;

namespace CVScore.Domain.Entities;

public class InterviewProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid CvDocumentId { get; set; }
    public string TargetRole { get; set; } = string.Empty;
    public InterviewLevel TargetLevel { get; set; } = InterviewLevel.Junior;
    public InterviewLanguage Language { get; set; } = InterviewLanguage.English;
    public string? CompanyName { get; set; }
    public string? JobDescription { get; set; }
    public string? TechStack { get; set; }
    public string? FocusAreas { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;

    public User User { get; set; } = null!;
    public CvDocument CvDocument { get; set; } = null!;
    public ICollection<InterviewSession> InterviewSessions { get; set; } = new List<InterviewSession>();
}
