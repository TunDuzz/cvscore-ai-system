using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using CVScore.Infrastructure.AI.Configuration;
using Microsoft.Extensions.Options;

namespace CVScore.Infrastructure.AI.Gemini;

public class GeminiApiClient(HttpClient httpClient, IOptions<AiOptions> aiOptions) : IGeminiApiClient
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public async Task<string> GenerateJsonAsync(
        string prompt,
        JsonObject responseSchema,
        CancellationToken cancellationToken = default)
    {
        var options = aiOptions.Value.Gemini;
        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new InvalidOperationException("Gemini API key is not configured.");
        }

        var request = new GeminiGenerateContentRequest
        {
            Contents =
            [
                new GeminiContent
                {
                    Parts =
                    [
                        new GeminiPart
                        {
                            Text = prompt
                        }
                    ]
                }
            ],
            GenerationConfig = new GeminiGenerationConfig
            {
                ResponseMimeType = "application/json",
                ResponseJsonSchema = responseSchema
            }
        };

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            $"/v1beta/models/{options.Model}:generateContent");

        httpRequest.Headers.Add("x-goog-api-key", options.ApiKey);
        httpRequest.Content = JsonContent.Create(request, options: SerializerOptions);

        using var response = await httpClient.SendAsync(httpRequest, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Gemini API request failed with status {(int)response.StatusCode}: {responseContent}");
        }

        var payload = JsonSerializer.Deserialize<GeminiGenerateContentResponse>(responseContent, SerializerOptions)
            ?? throw new InvalidOperationException("Gemini API returned an empty response.");

        var text = payload.Candidates?
            .FirstOrDefault()?
            .Content?
            .Parts?
            .FirstOrDefault()?
            .Text;

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException("Gemini API returned no structured text content.");
        }

        return text;
    }

    private sealed class GeminiGenerateContentRequest
    {
        public IReadOnlyCollection<GeminiContent> Contents { get; set; } = [];
        public GeminiGenerationConfig? GenerationConfig { get; set; }
    }

    private sealed class GeminiContent
    {
        public string Role { get; set; } = "user";
        public IReadOnlyCollection<GeminiPart> Parts { get; set; } = [];
    }

    private sealed class GeminiPart
    {
        public string Text { get; set; } = string.Empty;
    }

    private sealed class GeminiGenerationConfig
    {
        public string ResponseMimeType { get; set; } = "application/json";
        public JsonObject? ResponseJsonSchema { get; set; }
    }

    private sealed class GeminiGenerateContentResponse
    {
        public IReadOnlyCollection<GeminiCandidate>? Candidates { get; set; }
    }

    private sealed class GeminiCandidate
    {
        public GeminiCandidateContent? Content { get; set; }
    }

    private sealed class GeminiCandidateContent
    {
        public IReadOnlyCollection<GeminiCandidatePart>? Parts { get; set; }
    }

    private sealed class GeminiCandidatePart
    {
        public string? Text { get; set; }
    }
}
