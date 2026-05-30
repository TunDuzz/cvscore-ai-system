namespace CVScore.Application.InterviewSessions.Dtos;

public class EvaluateInterviewAnswerRequest
{
    public decimal Score { get; set; }
    public string? Strengths { get; set; }
    public string? Improvements { get; set; }
    public string? SuggestedAnswer { get; set; }
    public string? DetailedAnalysis { get; set; }
}
