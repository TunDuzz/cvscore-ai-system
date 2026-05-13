using CVScore.Application.Abstractions.Persistence;
using CVScore.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVScore.Application.Auth.GetCurrentUser;

public class GetCurrentUserQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetCurrentUserQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(x => x.Id == request.UserId)
            .Select(x => new CurrentUserDto(
                x.Id,
                x.FullName,
                x.Email,
                x.AvatarUrl))
            .FirstOrDefaultAsync(cancellationToken);

        return user is null
            ? Result<CurrentUserDto>.Failure("User does not exist.")
            : Result<CurrentUserDto>.Success(user);
    }
}
