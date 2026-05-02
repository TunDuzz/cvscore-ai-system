using CVScore.Application.Abstractions.AI;
using CVScore.Domain.Enums;

namespace CVScore.Infrastructure.AI;

public class MockInterviewAnswerEvaluator : IInterviewAnswerEvaluator
{
    public Task<InterviewAnswerEvaluationResult> EvaluateAsync(
        InterviewAnswerEvaluationRequest request,
        CancellationToken cancellationToken = default)
    {
        var answerLength = request.AnswerContent.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        var hasStructure = request.AnswerContent.Contains("because", StringComparison.OrdinalIgnoreCase) ||
                           request.AnswerContent.Contains("for example", StringComparison.OrdinalIgnoreCase);
        var mentionsStack = !string.IsNullOrWhiteSpace(request.TechStack) &&
                            request.AnswerContent.Contains(
                                request.TechStack.Split(',')[0].Trim(),
                                StringComparison.OrdinalIgnoreCase);
        var isVietnamese = request.Language == InterviewLanguage.Vietnamese;

        var score = 55m;
        if (answerLength >= 40) score += 15m;
        if (answerLength >= 80) score += 10m;
        if (hasStructure) score += 10m;
        if (mentionsStack) score += 10m;
        if (score > 100m) score = 100m;

        var strengths = isVietnamese
            ? hasStructure
                ? "Câu trả lời có cấu trúc rõ ràng và thể hiện được phần nào lý do phía sau quyết định kỹ thuật."
                : "Câu trả lời khá ngắn gọn và vẫn bám đúng trọng tâm của câu hỏi phỏng vấn."
            : hasStructure
                ? "The answer has a clear structure and shows some reasoning behind the decision."
                : "The answer is concise and stays on the topic of the interview question.";

        var improvements = isVietnamese
            ? answerLength < 40
                ? "Nên bổ sung chiều sâu, ví dụ cụ thể và nêu rõ hơn các trade-off kỹ thuật."
                : "Nên làm câu trả lời cụ thể hơn bằng kết quả đo lường được và chi tiết triển khai."
            : answerLength < 40
                ? "Add more depth, concrete examples, and clearer technical tradeoffs."
                : "Make the answer more specific with measurable outcomes and implementation details.";

        var suggestedAnswer = isVietnamese
            ? $"Với vị trí {request.TargetRole}, một câu trả lời tốt hơn nên giải thích bối cảnh, quyết định kỹ thuật, trade-off và kết quả cuối cùng. " +
              $"Câu trả lời cũng nên bám sát câu hỏi '{request.QuestionContent}' và liên hệ tới công cụ hoặc thực hành phù hợp."
            : $"For a {request.TargetRole} role, a stronger answer should explain the context, technical decision, tradeoffs, and outcome. " +
              $"It should directly address the question '{request.QuestionContent}' and connect the answer to relevant tools or practices.";

        var detailedAnalysis = isVietnamese
            ? $"Trọng tâm kỳ vọng: {request.ExpectedAnswerPoints ?? "Lập luận rõ ràng, tính liên quan và kinh nghiệm thực tế"}. " +
              $"Độ dài câu trả lời quan sát được: {answerLength} từ. " +
              $"Mock evaluator dùng tín hiệu về cấu trúc, độ cụ thể và mức độ liên quan để đưa ra điểm số này."
            : $"Expected focus: {request.ExpectedAnswerPoints ?? "Clear reasoning, relevance, and practical experience"}. " +
              $"Observed answer length: {answerLength} words. " +
              $"The mock evaluator used structure, specificity, and relevance signals to produce this score.";

        return Task.FromResult(new InterviewAnswerEvaluationResult(
            score,
            strengths,
            improvements,
            suggestedAnswer,
            detailedAnalysis));
    }
}
