using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;

public sealed record EvaluateInterviewAnswerCommand(
    Guid InterviewAnswerId,
    decimal Score,
    string? Strengths,
    string? Improvements,
    string? SuggestedAnswer,
    string? DetailedAnalysis) : IRequest<Result<Guid>>;
