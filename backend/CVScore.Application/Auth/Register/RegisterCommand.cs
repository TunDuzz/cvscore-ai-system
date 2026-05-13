using CVScore.Application.Auth.Common;
using CVScore.Application.Common;
using MediatR;

namespace CVScore.Application.Auth.Register;

public sealed record RegisterCommand(
    string FullName,
    string Email,
    string Password) : IRequest<Result<AuthResponseDto>>;
