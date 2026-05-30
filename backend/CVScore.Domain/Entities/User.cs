namespace CVScore.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public ICollection<CvDocument> CvDocuments { get; set; } = new List<CvDocument>();
    public ICollection<InterviewProfile> InterviewProfiles { get; set; } = new List<InterviewProfile>();
}
