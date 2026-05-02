namespace CVScore.Application.Abstractions.AI;

public interface IInterviewAnswerEvaluator
{
    Task<InterviewAnswerEvaluationResult> EvaluateAsync(
        InterviewAnswerEvaluationRequest request,
        CancellationToken cancellationToken = default);
}

public sealed record InterviewAnswerEvaluationRequest(
    string TargetRole,
    CVScore.Domain.Enums.InterviewLanguage Language,
    string? TechStack,
    string QuestionContent,
    string? ExpectedAnswerPoints,
    string AnswerContent);

public sealed record InterviewAnswerEvaluationResult(
    decimal Score,
    string? Strengths,
    string? Improvements,
    string? SuggestedAnswer,
    string? DetailedAnalysis);
