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
        var isVietnamese = request.Language == InterviewLanguage.Vietnamese;

        var templates = isVietnamese
            ? new List<GeneratedInterviewQuestion>
            {
                new(
                    1,
                    QuestionCategory.Introduction,
                    QuestionDifficulty.Easy,
                    $"Hãy giới thiệu bản thân và giải thích vì sao bạn phù hợp với vị trí {role}.",
                    "Tóm tắt kinh nghiệm, kỹ năng liên quan và mức độ phù hợp với vị trí",
                    "Sinh từ vai trò mục tiêu và ngữ cảnh của ứng viên."),
                new(
                    2,
                    QuestionCategory.ProjectExperience,
                    QuestionDifficulty.Medium,
                    $"Hãy chia sẻ một dự án mà bạn đã dùng {stack} để giải quyết một bài toán thực tế.",
                    "Bối cảnh bài toán, quyết định kỹ thuật, cách triển khai và kết quả",
                    "Bám theo tech stack của ứng viên."),
                new(
                    3,
                    QuestionCategory.Technical,
                    QuestionDifficulty.Medium,
                    $"Nếu xây dựng các tính năng backend có khả năng mở rộng cho {company}, bạn sẽ cân nhắc những trade-off nào?",
                    "Scalability, maintainability, observability và trade-off khi triển khai",
                    "Điều chỉnh theo ngữ cảnh công ty mục tiêu."),
                new(
                    4,
                    QuestionCategory.Behavioral,
                    QuestionDifficulty.Medium,
                    "Hãy mô tả một lần bạn nhận được góp ý thẳng thắn và bạn đã phản hồi ra sao.",
                    "Khả năng tiếp nhận feedback, hành động cải thiện và kết quả",
                    "Đánh giá khả năng phát triển và hợp tác."),
                new(
                    5,
                    QuestionCategory.ProblemSolving,
                    QuestionDifficulty.Hard,
                    $"Nếu có sự cố production ảnh hưởng đến một tính năng của vị trí {role}, bạn sẽ điều tra và khôi phục hệ thống như thế nào?",
                    "Ưu tiên xử lý, debug, giao tiếp và phòng ngừa tái diễn",
                    "Đánh giá tư duy vận hành hệ thống.")
            }
            : new List<GeneratedInterviewQuestion>
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
