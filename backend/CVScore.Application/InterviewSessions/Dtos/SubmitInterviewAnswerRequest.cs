namespace CVScore.Application.InterviewSessions.Dtos;

public class SubmitInterviewAnswerRequest
{
    public Guid InterviewQuestionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? AudioUrl { get; set; }
    public int? DurationInSeconds { get; set; }
}
