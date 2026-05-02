using CVScore.Application.Abstractions.AI;
using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewSessions.GenerateInterviewQuestions;

public class GenerateInterviewQuestionsQueryHandler(
    IApplicationDbContext context,
    IInterviewQuestionGenerator questionGenerator)
    : IRequestHandler<GenerateInterviewQuestionsQuery, Result<IReadOnlyCollection<GeneratedInterviewQuestionDto>>>
{
    public async Task<Result<IReadOnlyCollection<GeneratedInterviewQuestionDto>>> Handle(
        GenerateInterviewQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var profile = await context.InterviewProfiles
            .Where(x => x.Id == request.InterviewProfileId && x.IsActive)
            .Select(x => new
            {
                x.TargetRole,
                x.TargetLevel,
                x.CompanyName,
                x.JobDescription,
                x.TechStack,
                x.FocusAreas,
                CvSummary = x.CvDocument.Summary
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (profile is null)
        {
            return Result<IReadOnlyCollection<GeneratedInterviewQuestionDto>>.Failure(
                "Interview profile does not exist or is inactive.");
        }

        var generated = await questionGenerator.GenerateAsync(
            new InterviewQuestionGenerationRequest(
                profile.TargetRole,
                profile.TargetLevel,
                profile.CompanyName,
                profile.JobDescription,
                profile.TechStack,
                profile.FocusAreas,
                profile.CvSummary,
                request.QuestionCount),
            cancellationToken);

        var result = generated
            .Select(x => new GeneratedInterviewQuestionDto(
                x.DisplayOrder,
                x.Category,
                x.Difficulty,
                x.Content,
                x.ExpectedAnswerPoints,
                x.AiRationale))
            .ToArray();

        return Result<IReadOnlyCollection<GeneratedInterviewQuestionDto>>.Success(result);
    }
}
