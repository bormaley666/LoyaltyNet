using System;
using System.Threading.Tasks;
using Hangfire;
using LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;
using LoyaltyNet.Infrastructure.BackgroundTasks.Executors;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Infrastructure.BackgroundTasks;

/// <inheritdoc/>
internal sealed class BackgroundTaskManager : IBackgroundTaskManager
{
    private readonly ILogger<BackgroundTaskManager> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BackgroundTaskManager(ILogger<BackgroundTaskManager> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundTaskPriority? priority = default)
        where TArgs : class
    {
        if (args is null)
        {
            throw new ArgumentNullException(
                nameof(args),
                $"Background task arguments for {typeof(TArgs).Name} cannot be null");
        }

        try
        {
            var jobId = EnqueueTask(args, priority ?? BackgroundTaskPriority.Normal);

            _logger.LogInformation(
                "Enqueued background task {JobId} of type {JobType}",
                jobId,
                typeof(TArgs).Name);

            return Task.FromResult(jobId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to enqueue background task of type {TaskType}",
                typeof(TArgs).Name);

            throw new BackgroundTaskException($"Failed to enqueue job: {ex.Message}", ex);
        }
    }

    /// <inheritdoc/>
    public Task<string> ScheduleAsync<TArgs>(
        TArgs args,
        TimeSpan delay,
        BackgroundTaskPriority? priority = default)
            where TArgs : class
    {
        return ScheduleAsync(args, DateTimeOffset.UtcNow.Add(delay), priority);
    }

    /// <inheritdoc/>
    public Task<string> ScheduleAsync<TArgs>(
        TArgs args,
        DateTimeOffset enqueueAt,
        BackgroundTaskPriority? priority = default)
            where TArgs : class
    {
        if (args is null)
        {
            throw new ArgumentNullException(
                nameof(args),
                $"Background task arguments for {typeof(TArgs).Name} cannot be null");
        }

        try
        {
            var jobId = ScheduleTask(
                args,
                priority ?? BackgroundTaskPriority.Normal,
                enqueueAt);

            _logger.LogInformation(
                "Scheduled background task {JobId} of type {JobType} at {EnqueueAt}",
                jobId,
                typeof(TArgs).Name,
                enqueueAt);

            return Task.FromResult(jobId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to schedule background task of type {TaskType} at {EnqueueAt}",
                typeof(TArgs).Name,
                enqueueAt);

            throw new BackgroundTaskException(
                $"Failed to schedule background task: {ex.Message}",
                ex);
        }
    }

    private static string EnqueueTask<TArgs>(TArgs args, BackgroundTaskPriority priority)
        where TArgs : class
    {
        var queue = BackgroundTaskConstants.GetQueue(priority);

        return BackgroundJob.Enqueue<BackgroundTaskExecutor<TArgs>>(
            queue,
            executor => executor.ExecuteAsync(args));
    }

    private static string ScheduleTask<TArgs>(
        TArgs args,
        BackgroundTaskPriority priority,
        DateTimeOffset enqueueAt)
            where TArgs : class
    {
        var queue = BackgroundTaskConstants.GetQueue(priority);

        return BackgroundJob.Schedule<BackgroundTaskExecutor<TArgs>>(
            queue,
            executor => executor.ExecuteAsync(args),
            enqueueAt);
    }
}
