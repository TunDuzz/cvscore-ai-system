using System.Text.Json.Nodes;

namespace CVScore.Infrastructure.AI.Gemini;

public interface IGeminiApiClient
{
    Task<string> GenerateJsonAsync(
        string prompt,
        JsonObject responseSchema,
        CancellationToken cancellationToken = default);
}
