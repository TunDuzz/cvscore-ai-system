using CVScore.Domain.Entities;

namespace CVScore.Application.Abstractions.Security;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(User user);
}

public sealed record JwtTokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc);
