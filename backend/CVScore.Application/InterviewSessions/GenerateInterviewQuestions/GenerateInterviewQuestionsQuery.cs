using CVScore.Application.Common;
using CVScore.Domain.Enums;
using MediatR;

namespace CVScore.Application.InterviewSessions.GenerateInterviewQuestions;

public sealed record GenerateInterviewQuestionsQuery(
    Guid InterviewProfileId,
    int QuestionCount = 5) : IRequest<Result<IReadOnlyCollection<GeneratedInterviewQuestionDto>>>;

public sealed record GeneratedInterviewQuestionDto(
    int DisplayOrder,
    QuestionCategory Category,
    QuestionDifficulty Difficulty,
    string Content,
    string? ExpectedAnswerPoints,
    string? AiRationale);
