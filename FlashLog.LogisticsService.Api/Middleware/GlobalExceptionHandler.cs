using FlashLog.LogisticsService.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace FlashLog.LogisticsService.Api.Middleware;

public class GlobalExceptionHandler
{
    public static async Task ConfigureExceptionHandler(HttpContext context)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionHandler>>();

        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandler?.Error;

        logger?.LogCritical(exception, "[GlobalExceptionFilter] Exception captured! TraceId: {traceId}", context.TraceIdentifier);

        context.Response.ContentType = "application/json";

        if (exception is CustomValidationException customValidationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var result = new
            {
                error = "Erro de validação.",
                message = customValidationException.Message,
                errors = customValidationException.Errors
            };
            await context.Response.WriteAsJsonAsync(result);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Internal Server Error",
                exception?.Message,
                TraceId = context.TraceIdentifier
            });
        }
    }
}