using System;
using LoyaltyNet.Infrastructure.Abstractions.DistributedLock;
using Medallion.Threading;
using Medallion.Threading.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Infrastructure.DistributedLock;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMedallionDistributedLock(
        this IServiceCollection services,
        Action<DistributedLockConfiguration> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        var configuration = new DistributedLockConfiguration();

        configure.Invoke(configuration);

        if (string.IsNullOrWhiteSpace(configuration.ConnectionString))
        {
            throw new InvalidOperationException(
                "A connection string is required to initialize the distributed lock provider. Call UsePostgres() in the configuration.");
        }

        services.AddSingleton<IDistributedLockManager, MedallionDistributedLockManager>();
        services.AddSingleton<IDistributedLockProvider>(_ =>
            new PostgresDistributedSynchronizationProvider(configuration.ConnectionString));

        return services;
    }
}
