namespace CVScore.Application.InterviewSessions.Dtos;

public class FeedbackDto
{
    public Guid Id { get; set; }
    public Guid InterviewAnswerId { get; set; }
    public decimal Score { get; set; }
    public string? Strengths { get; set; }
    public string? Improvements { get; set; }
    public string? SuggestedAnswer { get; set; }
    public string? DetailedAnalysis { get; set; }
    public DateTime EvaluatedAt { get; set; }
}
