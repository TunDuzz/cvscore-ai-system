namespace CVScore.Application.Auth.Common;

public sealed record AuthResponseDto(
    Guid UserId,
    string FullName,
    string Email,
    string? AvatarUrl,
    string AccessToken,
    DateTime ExpiresAtUtc);
