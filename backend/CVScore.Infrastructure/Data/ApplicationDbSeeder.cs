using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Data;

public static class ApplicationDbSeeder
{
    private static readonly Guid DemoUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid DemoCvDocumentId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid DemoInterviewProfileId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid DemoInterviewSessionId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid DemoQuestionOneId = Guid.Parse("55555555-5555-5555-5555-555555555555");
    private static readonly Guid DemoQuestionTwoId = Guid.Parse("66666666-6666-6666-6666-666666666666");

    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var user = new User
        {
            Id = DemoUserId,
            FullName = "Demo Candidate",
            Email = "demo@cvscore.local",
            PasswordHash = "seed-password-hash",
            AvatarUrl = "https://example.com/avatar.png"
        };

        var cv = new CvDocument
        {
            Id = DemoCvDocumentId,
            User = user,
            Title = "Backend Developer CV",
            FileName = "backend-developer-cv.pdf",
            FileUrl = "https://example.com/files/backend-developer-cv.pdf",
            FileType = "pdf",
            RawText = "Sample CV content",
            Summary = "3 years of .NET and React experience.",
            IsPrimary = true,
            AnalyzedAt = DateTime.UtcNow
        };

        var profile = new InterviewProfile
        {
            Id = DemoInterviewProfileId,
            User = user,
            CvDocument = cv,
            TargetRole = "Backend Developer",
            TargetLevel = InterviewLevel.Mid,
            Language = InterviewLanguage.English,
            CompanyName = "CVScore",
            JobDescription = "Build scalable APIs and AI-driven interview features.",
            TechStack = ".NET, EF Core, MySQL, React",
            FocusAreas = "Clean Architecture, performance, testing"
        };

        var session = new InterviewSession
        {
            Id = DemoInterviewSessionId,
            InterviewProfile = profile,
            Status = InterviewSessionStatus.InProgress,
            StartedAt = DateTime.UtcNow,
            Questions =
            [
                new InterviewQuestion
                {
                    Id = DemoQuestionOneId,
                    DisplayOrder = 1,
                    Category = QuestionCategory.Introduction,
                    Difficulty = QuestionDifficulty.Easy,
                    Content = "Introduce yourself and summarize your backend experience.",
                    ExpectedAnswerPoints = "Experience summary, key technologies, measurable impact"
                },
                new InterviewQuestion
                {
                    Id = DemoQuestionTwoId,
                    DisplayOrder = 2,
                    Category = QuestionCategory.Technical,
                    Difficulty = QuestionDifficulty.Medium,
                    Content = "How would you design a CQRS-based interview workflow in .NET?",
                    ExpectedAnswerPoints = "Separation of command/query, validation, persistence, scalability"
                }
            ]
        };

        await context.Users.AddAsync(user, cancellationToken);
        await context.CvDocuments.AddAsync(cv, cancellationToken);
        await context.InterviewProfiles.AddAsync(profile, cancellationToken);
        await context.InterviewSessions.AddAsync(session, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
