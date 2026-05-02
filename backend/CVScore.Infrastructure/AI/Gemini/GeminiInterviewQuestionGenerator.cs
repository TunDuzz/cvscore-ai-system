using CVScore.Application.Abstractions.AI;
using CVScore.Infrastructure.AI.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVScore.Infrastructure.AI.Gemini;

public class GeminiInterviewQuestionGenerator(
    IOptions<AiOptions> aiOptions,
    ILogger<GeminiInterviewQuestionGenerator> logger)
    : IInterviewQuestionGenerator
{
    public Task<IReadOnlyCollection<GeneratedInterviewQuestion>> GenerateAsync(
        InterviewQuestionGenerationRequest request,
        CancellationToken cancellationToken = default)
    {
        var options = aiOptions.Value.Gemini;

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new InvalidOperationException("Gemini API key is not configured.");
        }

        logger.LogInformation(
            "Gemini question generator placeholder invoked for role {TargetRole} using model {Model}.",
            request.TargetRole,
            options.Model);

        throw new NotImplementedException(
            "Gemini interview question generation is not implemented yet. Use the Mock provider or add the Gemini API client implementation.");
    }
}
