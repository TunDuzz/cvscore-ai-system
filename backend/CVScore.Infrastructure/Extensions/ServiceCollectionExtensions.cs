using CVScore.Application.CvDocuments.Services;
using CVScore.Application.InterviewProfiles.Services;
using CVScore.Application.InterviewSessions.Services;
using CVScore.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CVScore.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICvDocumentService, CvDocumentService>();
        services.AddScoped<IInterviewProfileService, InterviewProfileService>();
        services.AddScoped<IInterviewSessionService, InterviewSessionService>();

        return services;
    }
}
