using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Hangfire;
using LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Infrastructure.BackgroundTasks.Executors;

internal sealed class BackgroundTaskExecutor<TArgs>
    where TArgs : class
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<BackgroundTaskExecutor<TArgs>> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BackgroundTaskExecutor(IServiceScopeFactory serviceScopeFactory, ILogger<BackgroundTaskExecutor<TArgs>> logger)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [JobDisplayName("{0}")]
    [AutomaticRetry(
        Attempts = 3,
        OnAttemptsExceeded = AttemptsExceededAction.Fail,
        ExceptOn = [typeof(BackgroundTaskException)])]
    public async Task ExecuteAsync(TArgs args)
    {
        if (args is null)
        {
            throw new BackgroundTaskException(
                $"Task arguments for {typeof(TArgs).Name} cannot be null");
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("Starting execution task {args}", typeof(TArgs).Name);

            await ExecuteTaskAsync(args);

            stopwatch.Stop();

            _logger.LogInformation(
                "Successfully completed task {args} in {ElapsedMs} ms",
                typeof(TArgs).Name,
                stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(ex,
                "Failed to execute task {args} in {ElapsedMs} ms",
                typeof(TArgs).Name,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }

    private async Task ExecuteTaskAsync(TArgs args)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var backgroundTask = scope.ServiceProvider.GetService<IBackgroundTask<TArgs>>() 
            ?? throw new BackgroundTaskException($"No implementation found for IBackgroundTask<{typeof(TArgs).Name}>");

        await backgroundTask.ExecuteAsync(args);
    }
}