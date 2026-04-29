namespace CVScore.Domain.Entities;

public class InterviewAnswer : BaseEntity
{
    public Guid InterviewQuestionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? AudioUrl { get; set; }
    public int? DurationInSeconds { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public InterviewQuestion InterviewQuestion { get; set; } = null!;
    public Feedback? Feedback { get; set; }
}
