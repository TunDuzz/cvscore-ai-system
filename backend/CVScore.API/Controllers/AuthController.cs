using System.Security.Claims;
using CVScore.API.Contracts.Auth;
using CVScore.Application.Auth.GetCurrentUser;
using CVScore.Application.Auth.Login;
using CVScore.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[Route("api/auth")]
public class AuthController(ISender sender) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new RegisterCommand(request.FullName, request.Email, request.Password),
            cancellationToken);

        return FromResult(result, StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new LoginCommand(request.Email, request.Password),
            cancellationToken);

        return FromResult(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var result = await sender.Send(new GetCurrentUserQuery(userId), cancellationToken);
        return FromNullableResult(result);
    }
}
