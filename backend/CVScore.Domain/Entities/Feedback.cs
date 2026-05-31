namespace CVScore.Domain.Entities;

public class Feedback : BaseEntity
{
    public Guid InterviewAnswerId { get; set; }
    public decimal Score { get; set; }
    public string? Strengths { get; set; }
    public string? Improvements { get; set; }
    public string? SuggestedAnswer { get; set; }
    public string? DetailedAnalysis { get; set; }
    public DateTime EvaluatedAt { get; set; } = DateTime.UtcNow;

    public InterviewAnswer InterviewAnswer { get; set; } = null!;
}
