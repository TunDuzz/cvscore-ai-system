using CVScore.Application.Abstractions.AI;
using CVScore.Domain.Enums;

namespace CVScore.Infrastructure.AI;

public class MockInterviewQuestionGenerator : IInterviewQuestionGenerator
{
    public Task<IReadOnlyCollection<GeneratedInterviewQuestion>> GenerateAsync(
        InterviewQuestionGenerationRequest request,
        CancellationToken cancellationToken = default)
    {
        var role = request.TargetRole.Trim();
        var stack = string.IsNullOrWhiteSpace(request.TechStack) ? "your main stack" : request.TechStack.Trim();
        var company = string.IsNullOrWhiteSpace(request.CompanyName) ? "the target company" : request.CompanyName.Trim();

        var templates = new List<GeneratedInterviewQuestion>
        {
            new(
                1,
                QuestionCategory.Introduction,
                QuestionDifficulty.Easy,
                $"Please introduce yourself and explain why you are a fit for the {role} role.",
                "Career summary, relevant experience, and role fit",
                "Generated from target role and candidate context."),
            new(
                2,
                QuestionCategory.ProjectExperience,
                QuestionDifficulty.Medium,
                $"Tell me about a project where you used {stack} to solve a meaningful problem.",
                "Problem context, technical decisions, implementation, and results",
                "Anchored to the candidate tech stack."),
            new(
                3,
                QuestionCategory.Technical,
                QuestionDifficulty.Medium,
                $"What tradeoffs would you consider when building scalable backend features for {company}?",
                "Scalability, maintainability, observability, and delivery tradeoffs",
                "Tailored to the target company context."),
            new(
                4,
                QuestionCategory.Behavioral,
                QuestionDifficulty.Medium,
                "Describe a time you received critical feedback and how you responded.",
                "Openness to feedback, action taken, measurable improvement",
                "Behavioral signal for growth and collaboration."),
            new(
                5,
                QuestionCategory.ProblemSolving,
                QuestionDifficulty.Hard,
                $"If a production issue impacted a {role} feature, how would you investigate and recover it?",
                "Prioritization, debugging, communication, and prevention",
                "Assesses operational thinking.")
        };

        var generated = templates
            .Take(request.QuestionCount)
            .Select((x, index) => x with { DisplayOrder = index + 1 })
            .ToArray();

        return Task.FromResult<IReadOnlyCollection<GeneratedInterviewQuestion>>(generated);
    }
}
