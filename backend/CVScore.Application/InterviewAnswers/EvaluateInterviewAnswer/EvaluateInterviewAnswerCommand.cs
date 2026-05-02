using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;

public sealed record EvaluateInterviewAnswerCommand(Guid InterviewAnswerId) : IRequest<Result<Guid>>;
