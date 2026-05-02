using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;

public class EvaluateInterviewAnswerCommandHandler(IApplicationDbContext context)
    : IRequestHandler<EvaluateInterviewAnswerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(EvaluateInterviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var answerData = await context.InterviewAnswers
            .Where(x => x.Id == request.InterviewAnswerId)
            .Select(x => new
            {
                x.Id,
                SessionStatus = x.InterviewQuestion.InterviewSession.Status
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (answerData is null)
        {
            return Result<Guid>.Failure("Interview answer does not exist.");
        }

        if (answerData.SessionStatus != InterviewSessionStatus.InProgress &&
            answerData.SessionStatus != InterviewSessionStatus.Completed)
        {
            return Result<Guid>.Failure("Feedback can only be created for in-progress or completed sessions.");
        }

        var feedback = await context.Feedbacks
            .FirstOrDefaultAsync(x => x.InterviewAnswerId == request.InterviewAnswerId, cancellationToken);

        if (feedback is null)
        {
            feedback = new Feedback
            {
                InterviewAnswerId = request.InterviewAnswerId
            };

            await context.AddAsync(feedback, cancellationToken);
        }

        feedback.Score = request.Score;
        feedback.Strengths = request.Strengths?.Trim();
        feedback.Improvements = request.Improvements?.Trim();
        feedback.SuggestedAnswer = request.SuggestedAnswer?.Trim();
        feedback.DetailedAnalysis = request.DetailedAnalysis?.Trim();
        feedback.EvaluatedAt = DateTime.UtcNow;
        feedback.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(feedback.Id);
    }
}
