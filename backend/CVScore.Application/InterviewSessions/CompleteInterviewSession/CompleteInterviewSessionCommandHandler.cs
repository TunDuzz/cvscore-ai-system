using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using CVScore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewSessions.CompleteInterviewSession;

public class CompleteInterviewSessionCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CompleteInterviewSessionCommand, Result>
{
    public async Task<Result> Handle(CompleteInterviewSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await context.InterviewSessions
            .FirstOrDefaultAsync(x => x.Id == request.InterviewSessionId, cancellationToken);

        if (session is null)
        {
            return Result.Failure("Interview session does not exist.");
        }

        if (session.Status != InterviewSessionStatus.InProgress)
        {
            return Result.Failure("Only in-progress interview sessions can be completed.");
        }

        session.Status = InterviewSessionStatus.Completed;
        session.CompletedAt = DateTime.UtcNow;
        session.OverallScore = request.OverallScore;
        session.Summary = request.Summary?.Trim();

        if (session.StartedAt.HasValue)
        {
            session.DurationInSeconds = Math.Max(
                0,
                (int)(session.CompletedAt.Value - session.StartedAt.Value).TotalSeconds);
        }

        session.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
