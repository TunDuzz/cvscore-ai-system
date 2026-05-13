using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.Auth.GetCurrentUser;

public sealed record GetCurrentUserQuery(Guid UserId) : IRequest<Result<CurrentUserDto>>;

public sealed record CurrentUserDto(
    Guid UserId,
    string FullName,
    string Email,
    string? AvatarUrl);
