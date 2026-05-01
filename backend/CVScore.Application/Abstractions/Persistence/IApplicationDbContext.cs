using CVScore.Domain.Entities;

namespace CVScore.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    IQueryable<User> Users { get; }
    IQueryable<CvDocument> CvDocuments { get; }
    IQueryable<InterviewProfile> InterviewProfiles { get; }
    IQueryable<InterviewSession> InterviewSessions { get; }
    IQueryable<InterviewQuestion> InterviewQuestions { get; }
    IQueryable<InterviewAnswer> InterviewAnswers { get; }
    IQueryable<Feedback> Feedbacks { get; }

    Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
