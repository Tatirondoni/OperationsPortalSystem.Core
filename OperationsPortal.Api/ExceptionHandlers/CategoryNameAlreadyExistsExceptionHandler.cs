using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using OperationsPortal.Application.Exceptions;

namespace OperationsPortal.Api.ExceptionHandlers;

public sealed class CategoryNameAlreadyExistsExceptionHandler
    : IExceptionHandler
{
    private readonly ILogger<CategoryNameAlreadyExistsExceptionHandler> _logger;

    public CategoryNameAlreadyExistsExceptionHandler(
        ILogger<CategoryNameAlreadyExistsExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not CategoryNameAlreadyExistsException duplicate)
        {
            return false;
        }

        _logger.LogWarning(
            exception,
            "Tentativa de criar categoria duplicada: {CategoryName}",
            duplicate.Name);

        httpContext.Response.StatusCode =
            StatusCodes.Status409Conflict;

        httpContext.Response.ContentType =
            "application/problem+json";

        var response = new
        {
            type = "https://httpstatuses.com/409",
            title = "Nome de categoria já utilizado.",
            status = StatusCodes.Status409Conflict,
            detail = duplicate.Message,
            traceId = httpContext.TraceIdentifier
        };

        await httpContext.Response.WriteAsync(
            JsonSerializer.Serialize(response),
            cancellationToken);

        return true;
    }
}
