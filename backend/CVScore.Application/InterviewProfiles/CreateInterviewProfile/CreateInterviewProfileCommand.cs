using CVScore.Application.Common;
using CVScore.Domain.Enums;
using MediatR;

namespace CVScore.Application.InterviewProfiles.CreateInterviewProfile;

public sealed record CreateInterviewProfileCommand(
    Guid UserId,
    Guid CvDocumentId,
    string TargetRole,
    InterviewLevel TargetLevel,
    string? CompanyName,
    string? JobDescription,
    string? TechStack,
    string? FocusAreas,
    string? Notes) : IRequest<Result<Guid>>;
