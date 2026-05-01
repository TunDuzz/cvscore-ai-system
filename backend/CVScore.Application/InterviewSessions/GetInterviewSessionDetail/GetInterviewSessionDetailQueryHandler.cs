using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewSessions.GetInterviewSessionDetail;

public class GetInterviewSessionDetailQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetInterviewSessionDetailQuery, Result<InterviewSessionDetailDto>>
{
    public async Task<Result<InterviewSessionDetailDto>> Handle(
        GetInterviewSessionDetailQuery request,
        CancellationToken cancellationToken)
    {
        var session = await context.InterviewSessions
            .Where(x => x.Id == request.InterviewSessionId)
            .Select(x => new InterviewSessionDetailDto(
                x.Id,
                x.InterviewProfileId,
                x.Status,
                x.StartedAt,
                x.CompletedAt,
                x.DurationInSeconds,
                x.OverallScore,
                x.Summary,
                x.Questions
                    .OrderBy(q => q.DisplayOrder)
                    .Select(q => new InterviewSessionQuestionDto(
                        q.Id,
                        q.DisplayOrder,
                        q.Category,
                        q.Difficulty,
                        q.Content,
                        q.ExpectedAnswerPoints,
                        q.AiRationale,
                        q.Answers
                            .OrderBy(a => a.SubmittedAt)
                            .Select(a => new InterviewSessionAnswerDto(
                                a.Id,
                                a.Content,
                                a.AudioUrl,
                                a.DurationInSeconds,
                                a.SubmittedAt,
                                a.Feedback == null
                                    ? null
                                    : new InterviewSessionFeedbackDto(
                                        a.Feedback.Id,
                                        a.Feedback.Score,
                                        a.Feedback.Strengths,
                                        a.Feedback.Improvements,
                                        a.Feedback.SuggestedAnswer,
                                        a.Feedback.DetailedAnalysis,
                                        a.Feedback.EvaluatedAt)))
                            .ToList()))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            return Result<InterviewSessionDetailDto>.Failure("Interview session does not exist.");
        }

        return Result<InterviewSessionDetailDto>.Success(session);
    }
}
