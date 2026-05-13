using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using CVScore.Infrastructure.AI.Configuration;
using CVScore.Infrastructure.AI.Exceptions;
using CVScore.Infrastructure.AI.Gemini;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace CVScore.Infrastructure.Tests.AI.Gemini;

public class GeminiApiClientTests
{
    [Fact]
    public async Task GenerateJsonAsync_ReturnsStructuredText_WhenGeminiRespondsWithCandidateText()
    {
        var responseJson =
            """
            {
              "candidates": [
                {
                  "content": {
                    "parts": [
                      {
                        "text": "{\"questions\":[{\"displayOrder\":1,\"category\":\"Technical\",\"difficulty\":\"Medium\",\"content\":\"Sample question\",\"expectedAnswerPoints\":\"Points\",\"aiRationale\":\"Why\"}]}"
                      }
                    ]
                  }
                }
              ]
            }
            """;

        var client = CreateClient(HttpStatusCode.OK, responseJson);

        var result = await client.GenerateJsonAsync("prompt", new JsonObject(), CancellationToken.None);

        Assert.Contains("\"questions\"", result);
    }

    [Fact]
    public async Task GenerateJsonAsync_ThrowsAiProviderException_WhenStructuredTextIsMissing()
    {
        var responseJson =
            """
            {
              "candidates": [
                {
                  "content": {
                    "parts": [
                      {
                        "text": ""
                      }
                    ]
                  }
                }
              ]
            }
            """;

        var client = CreateClient(HttpStatusCode.OK, responseJson);

        var action = () => client.GenerateJsonAsync("prompt", new JsonObject(), CancellationToken.None);

        var exception = await Assert.ThrowsAsync<AiProviderException>(action);
        Assert.Contains("no structured text content", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task GenerateJsonAsync_ThrowsAiProviderException_WhenGeminiReturnsErrorStatus()
    {
        var client = CreateClient(HttpStatusCode.BadRequest, "{\"error\":\"invalid request\"}");

        var action = () => client.GenerateJsonAsync("prompt", new JsonObject(), CancellationToken.None);

        var exception = await Assert.ThrowsAsync<AiProviderException>(action);
        Assert.Contains("status 400", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    private static GeminiApiClient CreateClient(HttpStatusCode statusCode, string responseBody)
    {
        var httpClient = new HttpClient(new StubHttpMessageHandler(statusCode, responseBody))
        {
            BaseAddress = new Uri("https://generativelanguage.googleapis.com")
        };

        var options = Options.Create(new AiOptions
        {
            Gemini = new GeminiOptions
            {
                ApiKey = "test-key",
                Model = "gemini-test",
                BaseUrl = "https://generativelanguage.googleapis.com",
                TimeoutSeconds = 30
            }
        });

        return new GeminiApiClient(httpClient, options, NullLogger<GeminiApiClient>.Instance);
    }

    private sealed class StubHttpMessageHandler(HttpStatusCode statusCode, string responseBody) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
            };

            return Task.FromResult(response);
        }
    }
}
