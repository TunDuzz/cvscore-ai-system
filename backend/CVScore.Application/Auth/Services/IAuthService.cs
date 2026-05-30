using CVScore.Application.Auth.Dtos;
using CVScore.Application.Common;

namespace CVScore.Application.Auth.Services;

public interface IAuthService
{
    Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<UserProfileDto>> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
