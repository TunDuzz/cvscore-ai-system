using CVScore.Application.Abstractions.AI;

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

        var score = 55m;
        if (answerLength >= 40) score += 15m;
        if (answerLength >= 80) score += 10m;
        if (hasStructure) score += 10m;
        if (mentionsStack) score += 10m;
        if (score > 100m) score = 100m;

        var strengths = hasStructure
            ? "The answer has a clear structure and shows some reasoning behind the decision."
            : "The answer is concise and stays on the topic of the interview question.";

        var improvements = answerLength < 40
            ? "Add more depth, concrete examples, and clearer technical tradeoffs."
            : "Make the answer more specific with measurable outcomes and implementation details.";

        var suggestedAnswer =
            $"For a {request.TargetRole} role, a stronger answer should explain the context, technical decision, tradeoffs, and outcome. " +
            $"It should directly address the question '{request.QuestionContent}' and connect the answer to relevant tools or practices.";

        var detailedAnalysis =
            $"Expected focus: {request.ExpectedAnswerPoints ?? "Clear reasoning, relevance, and practical experience"}. " +
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
