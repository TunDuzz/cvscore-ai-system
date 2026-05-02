using CVScore.Application.Abstractions.AI;
using CVScore.Infrastructure.AI.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVScore.Infrastructure.AI.Gemini;

public class GeminiInterviewAnswerEvaluator(
    IOptions<AiOptions> aiOptions,
    ILogger<GeminiInterviewAnswerEvaluator> logger)
    : IInterviewAnswerEvaluator
{
    public Task<InterviewAnswerEvaluationResult> EvaluateAsync(
        InterviewAnswerEvaluationRequest request,
        CancellationToken cancellationToken = default)
    {
        var options = aiOptions.Value.Gemini;

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new InvalidOperationException("Gemini API key is not configured.");
        }

        logger.LogInformation(
            "Gemini answer evaluator placeholder invoked for role {TargetRole} using model {Model}.",
            request.TargetRole,
            options.Model);

        throw new NotImplementedException(
            "Gemini interview answer evaluation is not implemented yet. Use the Mock provider or add the Gemini API client implementation.");
    }
}
