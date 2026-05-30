using CVScore.Domain.Enums;

namespace CVScore.Domain.Entities;

public class InterviewQuestion : BaseEntity
{
    public Guid InterviewSessionId { get; set; }
    public int DisplayOrder { get; set; }
    public QuestionCategory Category { get; set; } = QuestionCategory.Introduction;
    public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Medium;
    public string Content { get; set; } = string.Empty;
    public string? ExpectedAnswerPoints { get; set; }
    public string? AiRationale { get; set; }

    public InterviewSession InterviewSession { get; set; } = null!;
    public ICollection<InterviewAnswer> Answers { get; set; } = new List<InterviewAnswer>();
}
