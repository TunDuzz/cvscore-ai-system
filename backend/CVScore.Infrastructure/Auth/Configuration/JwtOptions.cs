namespace CVScore.Infrastructure.Auth.Configuration;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "CVScore";
    public string Audience { get; set; } = "CVScore.Client";
    public string SecretKey { get; set; } = "ReplaceWithAStrongSecretKeyForDevelopmentOnly123!";
    public int ExpiryMinutes { get; set; } = 120;
}
