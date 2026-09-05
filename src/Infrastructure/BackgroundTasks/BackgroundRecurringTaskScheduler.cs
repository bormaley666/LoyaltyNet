using System;
using Hangfire;
using LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;
using LoyaltyNet.Infrastructure.BackgroundTasks.Executors;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Infrastructure.BackgroundTasks;

/// <inheritdoc/>
internal sealed class BackgroundRecurringTaskScheduler : IBackgroundRecurringTaskScheduler
{
    private readonly ILogger<BackgroundRecurringTaskScheduler> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BackgroundRecurringTaskScheduler(ILogger<BackgroundRecurringTaskScheduler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public void Schedule<TTask>(string cronExpression)
        where TTask : IBackgroundRecurringTask
    {
        if (string.IsNullOrWhiteSpace(cronExpression))
        {
            throw new ArgumentException(
                "Cron expression cannot be null or whitespace",
                nameof(cronExpression));
        }

        try
        {
            var jobId = typeof(TTask).FullName!;

            ScheduleRecurringTask<TTask>(jobId, cronExpression);

            _logger.LogInformation(
                "Scheduled background recurring task {TaskType} with job ID {JobId} and cron expression {CronExpression}",
                typeof(TTask).Name,
                jobId,
                cronExpression);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to schedule background recurring task {TaskType} with cron expression {CronExpression}",
                typeof(TTask).Name,
                cronExpression);

            throw;
        }
    }

    private static void ScheduleRecurringTask<TTask>(string jobId, string cronExpression)
        where TTask : IBackgroundRecurringTask
    {
        RecurringJob.AddOrUpdate<BackgroundRecurringTaskExecutor<TTask>>(
            jobId,
            BackgroundTaskConstants.RecurringQueue,
            executor => executor.ExecuteAsync(),
            cronExpression,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });
    }
}
