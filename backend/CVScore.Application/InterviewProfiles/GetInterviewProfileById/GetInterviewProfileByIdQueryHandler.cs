using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewProfiles.GetInterviewProfileById;

public class GetInterviewProfileByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetInterviewProfileByIdQuery, Result<InterviewProfileDetailDto>>
{
    public async Task<Result<InterviewProfileDetailDto>> Handle(
        GetInterviewProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        var profile = await context.InterviewProfiles
            .Where(x => x.Id == request.InterviewProfileId)
            .Select(x => new InterviewProfileDetailDto(
                x.Id,
                x.UserId,
                x.CvDocumentId,
                x.TargetRole,
                x.TargetLevel,
                x.CompanyName,
                x.JobDescription,
                x.TechStack,
                x.FocusAreas,
                x.Notes,
                x.IsActive,
                x.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (profile is null)
        {
            return Result<InterviewProfileDetailDto>.Failure("Interview profile does not exist.");
        }

        return Result<InterviewProfileDetailDto>.Success(profile);
    }
}
