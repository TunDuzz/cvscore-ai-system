using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewSessions.StartInterviewSession;

public class StartInterviewSessionCommandHandler(IApplicationDbContext context)
    : IRequestHandler<StartInterviewSessionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(StartInterviewSessionCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await context.InterviewProfiles
            .AnyAsync(x => x.Id == request.InterviewProfileId && x.IsActive, cancellationToken);

        if (!profileExists)
        {
            return Result<Guid>.Failure("Interview profile does not exist or is inactive.");
        }

        if (request.Questions.Count == 0)
        {
            return Result<Guid>.Failure("At least one interview question is required.");
        }

        var normalizedQuestions = request.Questions
            .OrderBy(x => x.DisplayOrder)
            .ToList();

        var hasDuplicateOrder = normalizedQuestions
            .GroupBy(x => x.DisplayOrder)
            .Any(g => g.Count() > 1);

        if (hasDuplicateOrder)
        {
            return Result<Guid>.Failure("Question display order must be unique.");
        }

        var session = new InterviewSession
        {
            InterviewProfileId = request.InterviewProfileId,
            Status = InterviewSessionStatus.InProgress,
            StartedAt = DateTime.UtcNow,
            Questions = normalizedQuestions.Select(x => new InterviewQuestion
            {
                DisplayOrder = x.DisplayOrder,
                Category = x.Category,
                Difficulty = x.Difficulty,
                Content = x.Content.Trim(),
                ExpectedAnswerPoints = x.ExpectedAnswerPoints?.Trim(),
                AiRationale = x.AiRationale?.Trim()
            }).ToList()
        };

        await context.AddAsync(session, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(session.Id);
    }
}
