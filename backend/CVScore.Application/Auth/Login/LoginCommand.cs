using CVScore.Application.Auth.Common;
using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<Result<AuthResponseDto>>;
