using CVScore.Domain.Enums;

namespace CVScore.Application.InterviewSessions.Dtos;

public class InterviewSessionDetailDto
{
    public Guid Id { get; set; }
    public Guid InterviewProfileId { get; set; }
    public InterviewSessionStatus Status { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? DurationInSeconds { get; set; }
    public decimal? OverallScore { get; set; }
    public string? Summary { get; set; }
    public IReadOnlyList<InterviewQuestionDto> Questions { get; set; } = [];
}
