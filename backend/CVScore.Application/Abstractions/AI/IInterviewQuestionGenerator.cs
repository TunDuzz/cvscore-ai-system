using CVScore.Domain.Enums;

namespace CVScore.Application.Abstractions.AI;

public interface IInterviewQuestionGenerator
{
    Task<IReadOnlyCollection<GeneratedInterviewQuestion>> GenerateAsync(
        InterviewQuestionGenerationRequest request,
        CancellationToken cancellationToken = default);
}

public sealed record InterviewQuestionGenerationRequest(
    string TargetRole,
    InterviewLevel TargetLevel,
    string? CompanyName,
    string? JobDescription,
    string? TechStack,
    string? FocusAreas,
    string? CvSummary,
    int QuestionCount);

public sealed record GeneratedInterviewQuestion(
    int DisplayOrder,
    QuestionCategory Category,
    QuestionDifficulty Difficulty,
    string Content,
    string? ExpectedAnswerPoints,
    string? AiRationale);
