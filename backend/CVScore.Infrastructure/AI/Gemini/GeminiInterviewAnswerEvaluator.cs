using CVScore.Application.Abstractions.AI;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;

namespace CVScore.Infrastructure.AI.Gemini;

public class GeminiInterviewAnswerEvaluator(
    IGeminiApiClient geminiApiClient,
    ILogger<GeminiInterviewAnswerEvaluator> logger)
    : IInterviewAnswerEvaluator
{
    public Task<InterviewAnswerEvaluationResult> EvaluateAsync(
        InterviewAnswerEvaluationRequest request,
        CancellationToken cancellationToken = default) =>
        EvaluateInternalAsync(request, cancellationToken);

    private async Task<InterviewAnswerEvaluationResult> EvaluateInternalAsync(
        InterviewAnswerEvaluationRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Evaluating interview answer with Gemini for role {TargetRole}.",
            request.TargetRole);

        var prompt =
            $"""
            Evaluate this interview answer for a {request.TargetRole} role.

            Tech stack: {request.TechStack ?? "N/A"}
            Question: {request.QuestionContent}
            Expected answer points: {request.ExpectedAnswerPoints ?? "N/A"}
            Candidate answer: {request.AnswerContent}

            Return JSON only. Score must be from 0 to 100.
            """;

        var schema = new JsonObject
        {
            ["type"] = "object",
            ["properties"] = new JsonObject
            {
                ["score"] = new JsonObject { ["type"] = "number" },
                ["strengths"] = new JsonObject { ["type"] = new JsonArray("string", "null") },
                ["improvements"] = new JsonObject { ["type"] = new JsonArray("string", "null") },
                ["suggestedAnswer"] = new JsonObject { ["type"] = new JsonArray("string", "null") },
                ["detailedAnalysis"] = new JsonObject { ["type"] = new JsonArray("string", "null") }
            },
            ["required"] = new JsonArray("score", "strengths", "improvements", "suggestedAnswer", "detailedAnalysis")
        };

        var json = await geminiApiClient.GenerateJsonAsync(prompt, schema, cancellationToken);
        var payload = JsonSerializer.Deserialize<AnswerEvaluationPayload>(json)
            ?? throw new InvalidOperationException("Gemini returned invalid answer evaluation JSON.");

        return new InterviewAnswerEvaluationResult(
            Math.Clamp(payload.Score, 0m, 100m),
            payload.Strengths,
            payload.Improvements,
            payload.SuggestedAnswer,
            payload.DetailedAnalysis);
    }

    private sealed class AnswerEvaluationPayload
    {
        public decimal Score { get; set; }
        public string? Strengths { get; set; }
        public string? Improvements { get; set; }
        public string? SuggestedAnswer { get; set; }
        public string? DetailedAnalysis { get; set; }
    }
}
