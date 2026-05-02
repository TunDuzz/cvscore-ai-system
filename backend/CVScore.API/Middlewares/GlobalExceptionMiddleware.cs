using System.Net;
using System.Text.Json;
using CVScore.API.Contracts.Common;
using CVScore.Infrastructure.AI.Exceptions;
using FluentValidation;

namespace CVScore.API.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = ApiResponse.Fail(
                "validation_failed",
                "Validation failed.",
                ex.Errors.Select(x => new ApiValidationError
                {
                    Field = x.PropertyName,
                    Error = x.ErrorMessage
                }));

            response.Error!.TraceId = context.TraceIdentifier;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (AiProviderException ex)
        {
            logger.LogWarning(ex, "AI provider request failed.");

            context.Response.StatusCode = ex.Message.Contains("timed out", StringComparison.OrdinalIgnoreCase)
                ? (int)HttpStatusCode.GatewayTimeout
                : (int)HttpStatusCode.BadGateway;
            context.Response.ContentType = "application/json";

            var response = ApiResponse.Fail("ai_provider_error", ex.Message);
            response.Error!.TraceId = context.TraceIdentifier;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = ApiResponse.Fail("internal_server_error", "An unexpected error occurred.");
            response.Error!.TraceId = context.TraceIdentifier;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
