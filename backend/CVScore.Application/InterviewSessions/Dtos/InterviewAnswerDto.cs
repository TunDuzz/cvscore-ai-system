namespace CVScore.Application.InterviewSessions.Dtos;

public class InterviewAnswerDto
{
    public Guid Id { get; set; }
    public Guid InterviewQuestionId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? AudioUrl { get; set; }
    public int? DurationInSeconds { get; set; }
    public DateTime SubmittedAt { get; set; }
    public FeedbackDto? Feedback { get; set; }
}
