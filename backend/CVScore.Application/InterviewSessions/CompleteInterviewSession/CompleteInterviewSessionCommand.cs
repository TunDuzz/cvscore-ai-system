using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.InterviewSessions.CompleteInterviewSession;

public sealed record CompleteInterviewSessionCommand(
    Guid InterviewSessionId,
    decimal? OverallScore,
    string? Summary) : IRequest<Result>;
