using System;
using System.Linq;
using Hangfire;
using Hangfire.PostgreSql;
using LoyaltyNet.Infrastructure.Abstractions.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LoyaltyNet.Infrastructure.BackgroundTasks;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, Action<BackgroundTaskConfiguration> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var configuration = new BackgroundTaskConfiguration();

        configure.Invoke(configuration);

        if (string.IsNullOrWhiteSpace(configuration.ConnectionString))
        {
            throw new InvalidOperationException("Storage must be configured. Call UsePostgres() in the configuration");
        }

        services.TryAddHangfire(configuration);
        services.TryAddSingleton<IBackgroundTaskManager, BackgroundTaskManager>();
        services.TryAddSingleton<IBackgroundRecurringTaskScheduler, BackgroundRecurringTaskScheduler>();

        return services;
    }

    public static IServiceCollection AddBackgroundTask<TArgs, TTask>(this IServiceCollection services)
        where TTask : class, IBackgroundTask<TArgs>
    {
        services.TryAddTransient<TTask>();
        services.TryAddTransient<IBackgroundTask<TArgs>>(sp => sp.GetRequiredService<TTask>());

        return services;
    }

    public static IServiceCollection AddBackgroundRecurringTask<TTask>(this IServiceCollection services)
        where TTask : class, IBackgroundRecurringTask
    {
        services.TryAddTransient<TTask>();
        services.TryAddTransient<IBackgroundRecurringTask>(sp => sp.GetRequiredService<TTask>());

        return services;
    }

    private static void TryAddHangfire(this IServiceCollection services, BackgroundTaskConfiguration configuration)
    {
        if (services.Any(descriptor => descriptor.ServiceType == typeof(IGlobalConfiguration)))
        {
            return;
        }

        services.AddHangfire((serviceProvider, hangfireConfig) =>
        {
            hangfireConfig
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(
                    bootstrapperOptions => bootstrapperOptions.UseNpgsqlConnection(configuration.ConnectionString),
                    new PostgreSqlStorageOptions());
        });

        services.AddHangfireServer(options =>
        {
            options.Queues = BackgroundTaskConstants.Queues;
            options.WorkerCount = 10;
            options.ServerName = "background-tasks";
        });

        services.AddHangfireServer(options =>
        {
            options.Queues = [BackgroundTaskConstants.RecurringQueue];
            options.WorkerCount = 3;
            options.ServerName = "background-recurring-tasks";
        });
    }
}
