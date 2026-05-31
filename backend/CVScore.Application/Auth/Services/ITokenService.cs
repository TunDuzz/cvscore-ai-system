using CVScore.Application.Auth.Dtos;
using CVScore.Domain.Entities;

namespace CVScore.Application.Auth.Services;

public interface ITokenService
{
    AuthResponseDto CreateToken(User user);
}
