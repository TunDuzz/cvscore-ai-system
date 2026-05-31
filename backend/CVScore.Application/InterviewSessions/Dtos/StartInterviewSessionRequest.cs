using CVScore.Domain.Enums;

namespace CVScore.Application.InterviewSessions.Dtos;

public class StartInterviewSessionRequest
{
    public Guid InterviewProfileId { get; set; }
    public List<InterviewQuestionInputDto> Questions { get; set; } = [];
}

public class InterviewQuestionInputDto
{
    public int DisplayOrder { get; set; }
    public QuestionCategory Category { get; set; } = QuestionCategory.Introduction;
    public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Medium;
    public string Content { get; set; } = string.Empty;
    public string? ExpectedAnswerPoints { get; set; }
    public string? AiRationale { get; set; }
}
