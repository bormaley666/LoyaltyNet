using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Web.Infrastructure;

/// <summary>
/// Обработчик необработанных исключений
/// </summary>
public sealed class UnhandledExceptionHandler : IExceptionHandler
{
    private const string DefaultErrorMessage = "Внутренняя ошибка сервиса";
    private const string DefaultErrorCode = "internal_error";

    private readonly ILogger<UnhandledExceptionHandler> _logger;
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    public UnhandledExceptionHandler(ILogger<UnhandledExceptionHandler> logger, IWebHostEnvironment environment)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(exception);

        _logger.LogError(
            exception,
            "Unhandled exception occurred. Path: {Path}, Method: {Method}, Message: {Message}",
            httpContext.Request.Path,
            httpContext.Request.Method,
            exception.Message);

        var shouldIncludeException = !_environment.IsProduction();

        var response = new ProblemResponse(
            status: StatusCodes.Status500InternalServerError,
            message: DefaultErrorMessage,
            code: DefaultErrorCode,
            exception: shouldIncludeException ? exception : null);

        await WriteResponseAsync(httpContext.Response, response, token);

        return true;
    }

    private static async Task WriteResponseAsync(HttpResponse response, ProblemResponse problem, CancellationToken token)
    {
        response.StatusCode = problem.Status;
        response.ContentType = "application/json";

        await response.WriteAsync(problem.ToJson(), Encoding.UTF8, token);
    }
}
