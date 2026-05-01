using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewAnswers.SubmitInterviewAnswer;

public class SubmitInterviewAnswerCommandHandler(IApplicationDbContext context)
    : IRequestHandler<SubmitInterviewAnswerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(SubmitInterviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await context.InterviewQuestions
            .Where(x => x.Id == request.InterviewQuestionId)
            .Select(x => new
            {
                x.Id,
                SessionStatus = x.InterviewSession.Status
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (question is null)
        {
            return Result<Guid>.Failure("Interview question does not exist.");
        }

        if (question.SessionStatus != InterviewSessionStatus.InProgress)
        {
            return Result<Guid>.Failure("Only in-progress interview sessions can accept answers.");
        }

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            return Result<Guid>.Failure("Answer content is required.");
        }

        var answer = new InterviewAnswer
        {
            InterviewQuestionId = request.InterviewQuestionId,
            Content = request.Content.Trim(),
            AudioUrl = request.AudioUrl?.Trim(),
            DurationInSeconds = request.DurationInSeconds
        };

        await context.AddAsync(answer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(answer.Id);
    }
}
