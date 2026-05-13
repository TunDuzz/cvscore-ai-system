using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Abstractions.Security;
using CVScore.Application.Auth.Common;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.Auth.Register;

public class RegisterCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
{
    public async Task<Result<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var emailExists = await context.Users.AnyAsync(x => x.Email == normalizedEmail, cancellationToken);
        if (emailExists)
        {
            return Result<AuthResponseDto>.Failure("Email is already registered.");
        }

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = normalizedEmail,
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        await context.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var token = jwtTokenGenerator.GenerateToken(user);

        return Result<AuthResponseDto>.Success(new AuthResponseDto(
            user.Id,
            user.FullName,
            user.Email,
            user.AvatarUrl,
            token.AccessToken,
            token.ExpiresAtUtc));
    }
}
