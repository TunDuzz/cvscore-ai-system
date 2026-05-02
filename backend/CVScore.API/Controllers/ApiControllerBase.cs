using CVScore.API.Contracts.Common;
using CVScore.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult FromResult(Result result, int successStatusCode = StatusCodes.Status200OK)
    {
        if (result.IsSuccess)
        {
            if (successStatusCode == StatusCodes.Status204NoContent)
            {
                return StatusCode(successStatusCode);
            }

            return StatusCode(successStatusCode, ApiResponse.Succeed());
        }

        return BadRequest(ApiResponse.Fail("business_rule_violation", result.Error ?? "Request failed."));
    }

    protected IActionResult FromResult<T>(Result<T> result, Func<T, object>? map = null, int successStatusCode = StatusCodes.Status200OK)
    {
        if (result.IsSuccess && result.Value is not null)
        {
            var payload = map is null ? result.Value : map(result.Value);
            return StatusCode(successStatusCode, ApiResponse<object>.Succeed(payload));
        }

        return BadRequest(ApiResponse<object>.Fail("business_rule_violation", result.Error ?? "Request failed."));
    }

    protected IActionResult FromNullableResult<T>(Result<T> result, Func<T, object>? map = null)
    {
        if (result.IsSuccess && result.Value is not null)
        {
            var payload = map is null ? result.Value : map(result.Value);
            return Ok(ApiResponse<object>.Succeed(payload));
        }

        return NotFound(ApiResponse<object>.Fail("not_found", result.Error ?? "Resource was not found."));
    }
}
