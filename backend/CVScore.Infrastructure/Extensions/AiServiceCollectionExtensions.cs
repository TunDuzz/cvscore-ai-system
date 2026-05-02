using CVScore.Application.Abstractions.AI;
using CVScore.Infrastructure.AI;
using CVScore.Infrastructure.AI.Configuration;
using CVScore.Infrastructure.AI.Gemini;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CVScore.Infrastructure.Extensions;

public static class AiServiceCollectionExtensions
{
    public static IServiceCollection AddAiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AiOptions>(configuration.GetSection(AiOptions.SectionName));

        var aiOptions = configuration.GetSection(AiOptions.SectionName).Get<AiOptions>() ?? new AiOptions();
        var provider = aiOptions.Provider.Trim().ToLowerInvariant();

        switch (provider)
        {
            case "gemini":
                services.AddHttpClient<IGeminiApiClient, GeminiApiClient>((sp, client) =>
                {
                    var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<AiOptions>>().Value.Gemini;
                    client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/'));
                });
                services.AddScoped<IInterviewQuestionGenerator, GeminiInterviewQuestionGenerator>();
                services.AddScoped<IInterviewAnswerEvaluator, GeminiInterviewAnswerEvaluator>();
                break;

            case "mock":
            default:
                services.AddScoped<IInterviewQuestionGenerator, MockInterviewQuestionGenerator>();
                services.AddScoped<IInterviewAnswerEvaluator, MockInterviewAnswerEvaluator>();
                break;
        }

        return services;
    }
}
