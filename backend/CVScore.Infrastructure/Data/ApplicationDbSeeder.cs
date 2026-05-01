using CVScore.Domain.Entities;
using CVScore.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Data;

public static class ApplicationDbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var user = new User
        {
            FullName = "Demo Candidate",
            Email = "demo@cvscore.local",
            PasswordHash = "seed-password-hash",
            AvatarUrl = "https://example.com/avatar.png"
        };

        var cv = new CvDocument
        {
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
            User = user,
            CvDocument = cv,
            TargetRole = "Backend Developer",
            TargetLevel = InterviewLevel.Mid,
            CompanyName = "CVScore",
            JobDescription = "Build scalable APIs and AI-driven interview features.",
            TechStack = ".NET, EF Core, MySQL, React",
            FocusAreas = "Clean Architecture, performance, testing"
        };

        var session = new InterviewSession
        {
            InterviewProfile = profile,
            Status = InterviewSessionStatus.InProgress,
            StartedAt = DateTime.UtcNow,
            Questions =
            [
                new InterviewQuestion
                {
                    DisplayOrder = 1,
                    Category = QuestionCategory.Introduction,
                    Difficulty = QuestionDifficulty.Easy,
                    Content = "Introduce yourself and summarize your backend experience.",
                    ExpectedAnswerPoints = "Experience summary, key technologies, measurable impact"
                },
                new InterviewQuestion
                {
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
