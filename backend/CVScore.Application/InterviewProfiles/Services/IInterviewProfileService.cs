using CVScore.Application.Common;
using CVScore.Application.InterviewProfiles.Dtos;

namespace CVScore.Application.InterviewProfiles.Services;

public interface IInterviewProfileService
{
    Task<ServiceResult<InterviewProfileDto>> CreateAsync(CreateInterviewProfileRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<InterviewProfileDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
