using CVScore.Domain.Enums;

namespace CVScore.Domain.Entities;

public class InterviewSession : BaseEntity
{
    public Guid InterviewProfileId { get; set; }
    public InterviewSessionStatus Status { get; set; } = InterviewSessionStatus.Draft;
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? DurationInSeconds { get; set; }
    public decimal? OverallScore { get; set; }
    public string? Summary { get; set; }

    public InterviewProfile InterviewProfile { get; set; } = null!;
    public ICollection<InterviewQuestion> Questions { get; set; } = new List<InterviewQuestion>();
}
