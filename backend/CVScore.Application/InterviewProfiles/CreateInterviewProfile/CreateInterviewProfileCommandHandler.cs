using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.InterviewProfiles.CreateInterviewProfile;

public class CreateInterviewProfileCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateInterviewProfileCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateInterviewProfileCommand request, CancellationToken cancellationToken)
    {
        var userExists = await context.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken);
        if (!userExists)
        {
            return Result<Guid>.Failure("User does not exist.");
        }

        var cv = await context.CvDocuments
            .FirstOrDefaultAsync(x => x.Id == request.CvDocumentId && x.UserId == request.UserId, cancellationToken);

        if (cv is null)
        {
            return Result<Guid>.Failure("CV document does not exist or does not belong to the user.");
        }

        var profile = new InterviewProfile
        {
            UserId = request.UserId,
            CvDocumentId = request.CvDocumentId,
            TargetRole = request.TargetRole.Trim(),
            TargetLevel = request.TargetLevel,
            CompanyName = request.CompanyName?.Trim(),
            JobDescription = request.JobDescription?.Trim(),
            TechStack = request.TechStack?.Trim(),
            FocusAreas = request.FocusAreas?.Trim(),
            Notes = request.Notes?.Trim()
        };

        await context.AddAsync(profile, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(profile.Id);
    }
}
