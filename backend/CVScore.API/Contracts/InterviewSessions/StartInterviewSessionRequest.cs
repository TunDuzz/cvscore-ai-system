using CVScore.Domain.Enums;

namespace CVScore.API.Contracts.InterviewSessions;

public sealed class StartInterviewSessionRequest
{
    public Guid InterviewProfileId { get; set; }
    public IReadOnlyCollection<StartInterviewSessionQuestionRequest> Questions { get; set; }
        = Array.Empty<StartInterviewSessionQuestionRequest>();
}

public sealed class StartInterviewSessionQuestionRequest
{
    public int DisplayOrder { get; set; }
    public QuestionCategory Category { get; set; }
    public QuestionDifficulty Difficulty { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? ExpectedAnswerPoints { get; set; }
    public string? AiRationale { get; set; }
}
