using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Hangfire;
using LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Infrastructure.BackgroundTasks.Executors;

internal sealed class BackgroundRecurringTaskExecutor<TTask>
    where TTask : IBackgroundRecurringTask
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<BackgroundRecurringTaskExecutor<TTask>> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BackgroundRecurringTaskExecutor(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<BackgroundRecurringTaskExecutor<TTask>> logger)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [AutomaticRetry(Attempts = 0)]
    [DisableConcurrentExecution(timeoutInSeconds: 30)]
    public async Task ExecuteAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("Starting background recurring task {TaskType}", typeof(TTask).Name);

            await ExecuteTaskAsync();

            stopwatch.Stop();

            _logger.LogInformation(
                "Successfully completed background recurring task {TaskType} in {ElapsedMilliseconds} ms",
                typeof(TTask).Name,
                stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(ex,
                "Background recurring task {TaskType} failed after {ElapsedMilliseconds} ms",
                typeof(TTask).Name,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }

    private async Task ExecuteTaskAsync()
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var task = scope.ServiceProvider.GetService<TTask>()
            ?? throw new BackgroundTaskException(
                $"Background recurring task '{typeof(TTask).Name}' could not be resolved. " +
                $"Ensure the service is registered in the dependency injection container");

        await task.ExecuteAsync();
    }
}
