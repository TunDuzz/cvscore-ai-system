using CVScore.Application.Abstractions.AI;
using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewAnswers.EvaluateInterviewAnswer;

public class EvaluateInterviewAnswerCommandHandler(
    IApplicationDbContext context,
    IInterviewAnswerEvaluator answerEvaluator)
    : IRequestHandler<EvaluateInterviewAnswerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(EvaluateInterviewAnswerCommand request, CancellationToken cancellationToken)
    {
        var answerData = await context.InterviewAnswers
            .Where(x => x.Id == request.InterviewAnswerId)
            .Select(x => new
            {
                x.Id,
                x.Content,
                QuestionContent = x.InterviewQuestion.Content,
                x.InterviewQuestion.ExpectedAnswerPoints,
                SessionStatus = x.InterviewQuestion.InterviewSession.Status,
                TargetRole = x.InterviewQuestion.InterviewSession.InterviewProfile.TargetRole,
                Language = x.InterviewQuestion.InterviewSession.InterviewProfile.Language,
                TechStack = x.InterviewQuestion.InterviewSession.InterviewProfile.TechStack
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

        var evaluation = await answerEvaluator.EvaluateAsync(
            new InterviewAnswerEvaluationRequest(
                answerData.TargetRole,
                answerData.Language,
                answerData.TechStack,
                answerData.QuestionContent,
                answerData.ExpectedAnswerPoints,
                answerData.Content),
            cancellationToken);

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

        feedback.Score = evaluation.Score;
        feedback.Strengths = evaluation.Strengths?.Trim();
        feedback.Improvements = evaluation.Improvements?.Trim();
        feedback.SuggestedAnswer = evaluation.SuggestedAnswer?.Trim();
        feedback.DetailedAnalysis = evaluation.DetailedAnalysis?.Trim();
        feedback.EvaluatedAt = DateTime.UtcNow;
        feedback.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(feedback.Id);
    }
}
