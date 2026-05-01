using CVScore.Application.Common;
using CVScore.Domain.Enums;
using MediatR;

namespace CVScore.Application.InterviewProfiles.GetInterviewProfileById;

public sealed record GetInterviewProfileByIdQuery(Guid InterviewProfileId) : IRequest<Result<InterviewProfileDetailDto>>;

public sealed record InterviewProfileDetailDto(
    Guid Id,
    Guid UserId,
    Guid CvDocumentId,
    string TargetRole,
    InterviewLevel TargetLevel,
    string? CompanyName,
    string? JobDescription,
    string? TechStack,
    string? FocusAreas,
    string? Notes,
    bool IsActive,
    DateTime CreatedAt);
