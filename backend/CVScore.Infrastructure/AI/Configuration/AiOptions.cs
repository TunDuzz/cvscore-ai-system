namespace CVScore.Infrastructure.AI.Configuration;

public class AiOptions
{
    public const string SectionName = "AI";

    public string Provider { get; set; } = "Mock";
    public GeminiOptions Gemini { get; set; } = new();
}

public class GeminiOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "gemini-1.5-flash";
    public string BaseUrl { get; set; } = "https://generativelanguage.googleapis.com";
}
