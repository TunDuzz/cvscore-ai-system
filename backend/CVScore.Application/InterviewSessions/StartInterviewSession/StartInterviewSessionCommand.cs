using CVScore.Application.Common;
using CVScore.Domain.Enums;
using MediatR;

namespace CVScore.Application.InterviewSessions.StartInterviewSession;

public sealed record StartInterviewSessionCommand(
    Guid InterviewProfileId,
    IReadOnlyCollection<StartInterviewSessionQuestionItem> Questions) : IRequest<Result<Guid>>;

public sealed record StartInterviewSessionQuestionItem(
    int DisplayOrder,
    QuestionCategory Category,
    QuestionDifficulty Difficulty,
    string Content,
    string? ExpectedAnswerPoints,
    string? AiRationale);
