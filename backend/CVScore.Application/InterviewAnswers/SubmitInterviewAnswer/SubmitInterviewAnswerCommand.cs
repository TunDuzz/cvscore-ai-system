using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.InterviewAnswers.SubmitInterviewAnswer;

public sealed record SubmitInterviewAnswerCommand(
    Guid InterviewQuestionId,
    string Content,
    string? AudioUrl,
    int? DurationInSeconds) : IRequest<Result<Guid>>;
