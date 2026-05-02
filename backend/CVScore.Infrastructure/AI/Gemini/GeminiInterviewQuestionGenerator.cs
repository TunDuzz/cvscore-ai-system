using CVScore.Application.Abstractions.AI;
using CVScore.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;

namespace CVScore.Infrastructure.AI.Gemini;

public class GeminiInterviewQuestionGenerator(
    IGeminiApiClient geminiApiClient,
    ILogger<GeminiInterviewQuestionGenerator> logger)
    : IInterviewQuestionGenerator
{
    public Task<IReadOnlyCollection<GeneratedInterviewQuestion>> GenerateAsync(
        InterviewQuestionGenerationRequest request,
        CancellationToken cancellationToken = default) =>
        GenerateInternalAsync(request, cancellationToken);

    private async Task<IReadOnlyCollection<GeneratedInterviewQuestion>> GenerateInternalAsync(
        InterviewQuestionGenerationRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Generating interview questions with Gemini for role {TargetRole}.",
            request.TargetRole);

        var prompt =
            $"""
            Generate {request.QuestionCount} tailored interview questions for this candidate.

            Target role: {request.TargetRole}
            Target level: {request.TargetLevel}
            Interview language: {request.Language}
            Company: {request.CompanyName ?? "N/A"}
            Job description: {request.JobDescription ?? "N/A"}
            Tech stack: {request.TechStack ?? "N/A"}
            Focus areas: {request.FocusAreas ?? "N/A"}
            CV summary: {request.CvSummary ?? "N/A"}

            Return concise, practical interview questions in JSON only.
            The question content, expectedAnswerPoints, and aiRationale must be written in the requested interview language.
            """;

        var schema = new JsonObject
        {
            ["type"] = "object",
            ["properties"] = new JsonObject
            {
                ["questions"] = new JsonObject
                {
                    ["type"] = "array",
                    ["items"] = new JsonObject
                    {
                        ["type"] = "object",
                        ["properties"] = new JsonObject
                        {
                            ["displayOrder"] = new JsonObject { ["type"] = "integer" },
                            ["category"] = new JsonObject { ["type"] = "string" },
                            ["difficulty"] = new JsonObject { ["type"] = "string" },
                            ["content"] = new JsonObject { ["type"] = "string" },
                            ["expectedAnswerPoints"] = new JsonObject { ["type"] = new JsonArray("string", "null") },
                            ["aiRationale"] = new JsonObject { ["type"] = new JsonArray("string", "null") }
                        },
                        ["required"] = new JsonArray("displayOrder", "category", "difficulty", "content", "expectedAnswerPoints", "aiRationale")
                    }
                }
            },
            ["required"] = new JsonArray("questions")
        };

        var json = await geminiApiClient.GenerateJsonAsync(prompt, schema, cancellationToken);
        var payload = JsonSerializer.Deserialize<QuestionGenerationPayload>(json)
            ?? throw new InvalidOperationException("Gemini returned invalid question generation JSON.");

        return payload.Questions
            .Take(request.QuestionCount)
            .Select(q => new GeneratedInterviewQuestion(
                q.DisplayOrder,
                ParseCategory(q.Category),
                ParseDifficulty(q.Difficulty),
                q.Content,
                q.ExpectedAnswerPoints,
                q.AiRationale))
            .ToArray();
    }

    private static QuestionCategory ParseCategory(string category) =>
        Enum.TryParse<QuestionCategory>(category, true, out var parsed)
            ? parsed
            : QuestionCategory.Technical;

    private static QuestionDifficulty ParseDifficulty(string difficulty) =>
        Enum.TryParse<QuestionDifficulty>(difficulty, true, out var parsed)
            ? parsed
            : QuestionDifficulty.Medium;

    private sealed class QuestionGenerationPayload
    {
        public IReadOnlyCollection<QuestionItem> Questions { get; set; } = [];
    }

    private sealed class QuestionItem
    {
        public int DisplayOrder { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ExpectedAnswerPoints { get; set; }
        public string? AiRationale { get; set; }
    }
}
