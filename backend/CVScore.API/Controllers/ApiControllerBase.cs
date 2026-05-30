using CVScore.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace CVScore.API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult FromResult(ServiceResult result)
    {
        if (result.IsSuccess)
        {
            return NoContent();
        }

        return result.ErrorCode switch
        {
            "not_found" => NotFound(result.ErrorMessage),
            "invalid_state" => Conflict(result.ErrorMessage),
            _ => BadRequest(result.ErrorMessage)
        };
    }

    protected ActionResult FromResult<T>(ServiceResult<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return result.ErrorCode switch
        {
            "not_found" => NotFound(result.ErrorMessage),
            "invalid_state" => Conflict(result.ErrorMessage),
            _ => BadRequest(result.ErrorMessage)
        };
    }

    protected ActionResult CreatedFromResult<T>(string actionName, object routeValues, ServiceResult<T> result)
    {
        if (!result.IsSuccess)
        {
            return FromResult(result);
        }

        return CreatedAtAction(actionName, routeValues, result.Data);
    }
}
