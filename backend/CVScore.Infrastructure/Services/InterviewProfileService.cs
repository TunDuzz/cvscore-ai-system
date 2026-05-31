using CVScore.Application.Common;
using CVScore.Application.InterviewProfiles.Dtos;
using CVScore.Application.InterviewProfiles.Services;
using CVScore.Domain.Entities;
using CVScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Services;

public class InterviewProfileService(ApplicationDbContext dbContext) : IInterviewProfileService
{
    public async Task<ServiceResult<InterviewProfileDto>> CreateAsync(CreateInterviewProfileRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId == Guid.Empty || request.CvDocumentId == Guid.Empty || string.IsNullOrWhiteSpace(request.TargetRole))
        {
            return ServiceResult<InterviewProfileDto>.Failure("validation_error", "UserId, CvDocumentId, and target role are required.");
        }

        var cvDocument = await dbContext.CvDocuments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CvDocumentId, cancellationToken);

        if (cvDocument is null || cvDocument.UserId != request.UserId)
        {
            return ServiceResult<InterviewProfileDto>.Failure("not_found", "CV document was not found for the specified user.");
        }

        var profile = new InterviewProfile
        {
            UserId = request.UserId,
            CvDocumentId = request.CvDocumentId,
            TargetRole = request.TargetRole.Trim(),
            TargetLevel = request.TargetLevel,
            Language = request.Language,
            CompanyName = request.CompanyName?.Trim(),
            JobDescription = request.JobDescription,
            TechStack = request.TechStack,
            FocusAreas = request.FocusAreas,
            Notes = request.Notes,
            IsActive = true
        };

        dbContext.InterviewProfiles.Add(profile);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<InterviewProfileDto>.Success(Map(profile));
    }

    public async Task<ServiceResult<InterviewProfileDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var profile = await dbContext.InterviewProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (profile is null)
        {
            return ServiceResult<InterviewProfileDto>.Failure("not_found", "Interview profile was not found.");
        }

        return ServiceResult<InterviewProfileDto>.Success(Map(profile));
    }

    private static InterviewProfileDto Map(InterviewProfile profile) => new()
    {
        Id = profile.Id,
        UserId = profile.UserId,
        CvDocumentId = profile.CvDocumentId,
        TargetRole = profile.TargetRole,
        TargetLevel = profile.TargetLevel,
        Language = profile.Language,
        CompanyName = profile.CompanyName,
        JobDescription = profile.JobDescription,
        TechStack = profile.TechStack,
        FocusAreas = profile.FocusAreas,
        Notes = profile.Notes,
        IsActive = profile.IsActive,
        CreatedAt = profile.CreatedAt
    };
}
