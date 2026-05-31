using CVScore.Domain.Enums;

namespace CVScore.Application.InterviewProfiles.Dtos;

public class InterviewProfileDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CvDocumentId { get; set; }
    public string TargetRole { get; set; } = string.Empty;
    public InterviewLevel TargetLevel { get; set; }
    public InterviewLanguage Language { get; set; }
    public string? CompanyName { get; set; }
    public string? JobDescription { get; set; }
    public string? TechStack { get; set; }
    public string? FocusAreas { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
