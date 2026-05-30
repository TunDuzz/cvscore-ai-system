using CVScore.Domain.Enums;

namespace CVScore.Application.InterviewSessions.Dtos;

public class InterviewQuestionDto
{
    public Guid Id { get; set; }
    public int DisplayOrder { get; set; }
    public QuestionCategory Category { get; set; }
    public QuestionDifficulty Difficulty { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? ExpectedAnswerPoints { get; set; }
    public string? AiRationale { get; set; }
    public IReadOnlyList<InterviewAnswerDto> Answers { get; set; } = [];
}
