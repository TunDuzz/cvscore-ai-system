using CVScore.Application.Common;
using CVScore.Application.InterviewSessions.Dtos;
using CVScore.Application.InterviewSessions.Services;
using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using CVScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Services;

public class InterviewSessionService(ApplicationDbContext dbContext) : IInterviewSessionService
{
    public async Task<ServiceResult<InterviewSessionDetailDto>> StartAsync(StartInterviewSessionRequest request, CancellationToken cancellationToken = default)
    {
        if (request.InterviewProfileId == Guid.Empty)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("validation_error", "InterviewProfileId is required.");
        }

        if (request.Questions.Count == 0)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("validation_error", "At least one question is required.");
        }

        if (request.Questions.Any(x => string.IsNullOrWhiteSpace(x.Content)))
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("validation_error", "Every question must have content.");
        }

        var hasDuplicateOrder = request.Questions
            .GroupBy(x => x.DisplayOrder)
            .Any(x => x.Key <= 0 || x.Count() > 1);

        if (hasDuplicateOrder)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("validation_error", "Question display order must be unique and greater than zero.");
        }

        var profile = await dbContext.InterviewProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.InterviewProfileId, cancellationToken);

        if (profile is null || !profile.IsActive)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("not_found", "Active interview profile was not found.");
        }

        var session = new InterviewSession
        {
            InterviewProfileId = request.InterviewProfileId,
            Status = InterviewSessionStatus.InProgress,
            StartedAt = DateTime.UtcNow,
            Questions = request.Questions
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new InterviewQuestion
                {
                    DisplayOrder = x.DisplayOrder,
                    Category = x.Category,
                    Difficulty = x.Difficulty,
                    Content = x.Content.Trim(),
                    ExpectedAnswerPoints = x.ExpectedAnswerPoints,
                    AiRationale = x.AiRationale
                })
                .ToList()
        };

        dbContext.InterviewSessions.Add(session);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(session.Id, cancellationToken);
    }

    public async Task<ServiceResult<InterviewSessionDetailDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var session = await dbContext.InterviewSessions
            .AsNoTracking()
            .Include(x => x.Questions.OrderBy(q => q.DisplayOrder))
                .ThenInclude(x => x.Answers)
                    .ThenInclude(x => x.Feedback)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (session is null)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("not_found", "Interview session was not found.");
        }

        return ServiceResult<InterviewSessionDetailDto>.Success(MapSession(session));
    }

    public async Task<ServiceResult<InterviewAnswerDto>> SubmitAnswerAsync(SubmitInterviewAnswerRequest request, CancellationToken cancellationToken = default)
    {
        if (request.InterviewQuestionId == Guid.Empty || string.IsNullOrWhiteSpace(request.Content))
        {
            return ServiceResult<InterviewAnswerDto>.Failure("validation_error", "InterviewQuestionId and content are required.");
        }

        var question = await dbContext.InterviewQuestions
            .Include(x => x.InterviewSession)
            .FirstOrDefaultAsync(x => x.Id == request.InterviewQuestionId, cancellationToken);

        if (question is null)
        {
            return ServiceResult<InterviewAnswerDto>.Failure("not_found", "Interview question was not found.");
        }

        if (question.InterviewSession.Status != InterviewSessionStatus.InProgress)
        {
            return ServiceResult<InterviewAnswerDto>.Failure("invalid_state", "Only in-progress sessions can receive answers.");
        }

        var answer = new InterviewAnswer
        {
            InterviewQuestionId = request.InterviewQuestionId,
            Content = request.Content.Trim(),
            AudioUrl = request.AudioUrl,
            DurationInSeconds = request.DurationInSeconds,
            SubmittedAt = DateTime.UtcNow
        };

        dbContext.InterviewAnswers.Add(answer);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<InterviewAnswerDto>.Success(MapAnswer(answer));
    }

    public async Task<ServiceResult<FeedbackDto>> EvaluateAnswerAsync(Guid answerId, EvaluateInterviewAnswerRequest request, CancellationToken cancellationToken = default)
    {
        if (answerId == Guid.Empty)
        {
            return ServiceResult<FeedbackDto>.Failure("validation_error", "AnswerId is required.");
        }

        var answer = await dbContext.InterviewAnswers
            .Include(x => x.InterviewQuestion)
                .ThenInclude(x => x.InterviewSession)
            .Include(x => x.Feedback)
            .FirstOrDefaultAsync(x => x.Id == answerId, cancellationToken);

        if (answer is null)
        {
            return ServiceResult<FeedbackDto>.Failure("not_found", "Interview answer was not found.");
        }

        if (answer.InterviewQuestion.InterviewSession.Status == InterviewSessionStatus.Cancelled)
        {
            return ServiceResult<FeedbackDto>.Failure("invalid_state", "Cancelled sessions cannot be evaluated.");
        }

        if (request.Score < 0 || request.Score > 10)
        {
            return ServiceResult<FeedbackDto>.Failure("validation_error", "Score must be between 0 and 10.");
        }

        if (answer.Feedback is null)
        {
            answer.Feedback = new Feedback
            {
                InterviewAnswerId = answer.Id
            };
            dbContext.Feedbacks.Add(answer.Feedback);
        }

        answer.Feedback.Score = request.Score;
        answer.Feedback.Strengths = request.Strengths;
        answer.Feedback.Improvements = request.Improvements;
        answer.Feedback.SuggestedAnswer = request.SuggestedAnswer;
        answer.Feedback.DetailedAnalysis = request.DetailedAnalysis;
        answer.Feedback.EvaluatedAt = DateTime.UtcNow;
        answer.Feedback.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<FeedbackDto>.Success(MapFeedback(answer.Feedback));
    }

    public async Task<ServiceResult<InterviewSessionDetailDto>> CompleteAsync(Guid sessionId, CompleteInterviewSessionRequest request, CancellationToken cancellationToken = default)
    {
        var session = await dbContext.InterviewSessions
            .FirstOrDefaultAsync(x => x.Id == sessionId, cancellationToken);

        if (session is null)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("not_found", "Interview session was not found.");
        }

        if (session.Status != InterviewSessionStatus.InProgress)
        {
            return ServiceResult<InterviewSessionDetailDto>.Failure("invalid_state", "Only in-progress sessions can be completed.");
        }

        session.Status = InterviewSessionStatus.Completed;
        session.CompletedAt = DateTime.UtcNow;
        session.DurationInSeconds = session.StartedAt.HasValue
            ? Math.Max(0, Convert.ToInt32((session.CompletedAt.Value - session.StartedAt.Value).TotalSeconds))
            : null;
        session.OverallScore = request.OverallScore;
        session.Summary = request.Summary;
        session.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(session.Id, cancellationToken);
    }

    private static InterviewSessionDetailDto MapSession(InterviewSession session) => new()
    {
        Id = session.Id,
        InterviewProfileId = session.InterviewProfileId,
        Status = session.Status,
        StartedAt = session.StartedAt,
        CompletedAt = session.CompletedAt,
        DurationInSeconds = session.DurationInSeconds,
        OverallScore = session.OverallScore,
        Summary = session.Summary,
        Questions = session.Questions
            .OrderBy(x => x.DisplayOrder)
            .Select(MapQuestion)
            .ToList()
    };

    private static InterviewQuestionDto MapQuestion(InterviewQuestion question) => new()
    {
        Id = question.Id,
        DisplayOrder = question.DisplayOrder,
        Category = question.Category,
        Difficulty = question.Difficulty,
        Content = question.Content,
        ExpectedAnswerPoints = question.ExpectedAnswerPoints,
        AiRationale = question.AiRationale,
        Answers = question.Answers
            .OrderByDescending(x => x.SubmittedAt)
            .Select(MapAnswer)
            .ToList()
    };

    private static InterviewAnswerDto MapAnswer(InterviewAnswer answer) => new()
    {
        Id = answer.Id,
        InterviewQuestionId = answer.InterviewQuestionId,
        Content = answer.Content,
        AudioUrl = answer.AudioUrl,
        DurationInSeconds = answer.DurationInSeconds,
        SubmittedAt = answer.SubmittedAt,
        Feedback = answer.Feedback is null ? null : MapFeedback(answer.Feedback)
    };

    private static FeedbackDto MapFeedback(Feedback feedback) => new()
    {
        Id = feedback.Id,
        InterviewAnswerId = feedback.InterviewAnswerId,
        Score = feedback.Score,
        Strengths = feedback.Strengths,
        Improvements = feedback.Improvements,
        SuggestedAnswer = feedback.SuggestedAnswer,
        DetailedAnalysis = feedback.DetailedAnalysis,
        EvaluatedAt = feedback.EvaluatedAt
    };
}
