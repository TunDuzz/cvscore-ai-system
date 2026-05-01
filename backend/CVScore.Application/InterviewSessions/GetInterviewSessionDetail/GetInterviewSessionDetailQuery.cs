using CVScore.Application.Common;
using CVScore.Domain.Enums;
using MediatR;

namespace CVScore.Application.InterviewSessions.GetInterviewSessionDetail;

public sealed record GetInterviewSessionDetailQuery(Guid InterviewSessionId) : IRequest<Result<InterviewSessionDetailDto>>;

public sealed record InterviewSessionDetailDto(
    Guid Id,
    Guid InterviewProfileId,
    InterviewSessionStatus Status,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    int? DurationInSeconds,
    decimal? OverallScore,
    string? Summary,
    IReadOnlyCollection<InterviewSessionQuestionDto> Questions);

public sealed record InterviewSessionQuestionDto(
    Guid Id,
    int DisplayOrder,
    QuestionCategory Category,
    QuestionDifficulty Difficulty,
    string Content,
    string? ExpectedAnswerPoints,
    string? AiRationale,
    IReadOnlyCollection<InterviewSessionAnswerDto> Answers);

public sealed record InterviewSessionAnswerDto(
    Guid Id,
    string Content,
    string? AudioUrl,
    int? DurationInSeconds,
    DateTime SubmittedAt,
    InterviewSessionFeedbackDto? Feedback);

public sealed record InterviewSessionFeedbackDto(
    Guid Id,
    decimal Score,
    string? Strengths,
    string? Improvements,
    string? SuggestedAnswer,
    string? DetailedAnalysis,
    DateTime EvaluatedAt);
