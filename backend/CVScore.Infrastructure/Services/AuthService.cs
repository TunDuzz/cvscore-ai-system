using CVScore.Application.Auth.Dtos;
using CVScore.Application.Auth.Services;
using CVScore.Application.Common;
using CVScore.Domain.Entities;
using CVScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Infrastructure.Services;

public class AuthService(
    ApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    ITokenService tokenService) : IAuthService
{
    public async Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return ServiceResult<AuthResponseDto>.Failure("validation_error", "Full name, email, and password are required.");
        }

        if (request.Password.Length < 8)
        {
            return ServiceResult<AuthResponseDto>.Failure("validation_error", "Password must be at least 8 characters long.");
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var emailExists = await dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Email == normalizedEmail, cancellationToken);

        if (emailExists)
        {
            return ServiceResult<AuthResponseDto>.Failure("conflict", "Email is already registered.");
        }

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = normalizedEmail,
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<AuthResponseDto>.Success(tokenService.CreateToken(user));
    }

    public async Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return ServiceResult<AuthResponseDto>.Failure("validation_error", "Email and password are required.");
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == normalizedEmail, cancellationToken);

        if (user is null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return ServiceResult<AuthResponseDto>.Failure("unauthorized", "Invalid email or password.");
        }

        return ServiceResult<AuthResponseDto>.Success(tokenService.CreateToken(user));
    }

    public async Task<ServiceResult<UserProfileDto>> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return ServiceResult<UserProfileDto>.Failure("unauthorized", "User is not authenticated.");
        }

        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
        {
            return ServiceResult<UserProfileDto>.Failure("not_found", "User was not found.");
        }

        return ServiceResult<UserProfileDto>.Success(new UserProfileDto
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl
        });
    }
}
