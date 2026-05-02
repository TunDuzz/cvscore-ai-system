namespace CVScore.API.Contracts.InterviewSessions;

public sealed class CompleteInterviewSessionRequest
{
    public decimal? OverallScore { get; set; }
    public string? Summary { get; set; }
}
