using LoyaltyNet.Infrastructure.Abstractions.DistributedLock;
using Medallion.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.DistributedLock;

/// <inheritdoc/>
internal sealed class MedallionDistributedLockManager : IDistributedLockManager
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

    private readonly IDistributedLockProvider _provider;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="provider"></param>
    public MedallionDistributedLockManager(IDistributedLockProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    /// <inheritdoc/>
    public async Task<IDistributedLockHandle?> TryAcquireLockAsync(
        string resource,
        TimeSpan? timeout = default,
        CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);

        var effectiveTimeout = timeout ?? DefaultTimeout;

        var handle = await _provider.TryAcquireLockAsync(
            resource,
            effectiveTimeout,
            token
        );

        return handle is not null ? new MedallionDistributedLockerHandle(handle) : null;
    }

    /// <inheritdoc/>
    public async Task PerformActionWithLockAsync(
        string resource,
        Func<Task> action,
        TimeSpan? timeout = default,
        CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);
        ArgumentNullException.ThrowIfNull(action);

        var effectiveTimeout = timeout ?? DefaultTimeout;

        await using var handle = await _provider.AcquireLockAsync(
            resource,
            effectiveTimeout,
            token);

        await action();
    }

    /// <inheritdoc/>
    public async Task<TResult> PerformActionWithLockAsync<TResult>(
        string resource,
        Func<Task<TResult>> action,
        TimeSpan? timeout = default,
        CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);
        ArgumentNullException.ThrowIfNull(action);

        var effectiveTimeout = timeout ?? DefaultTimeout;

        await using var handle = await _provider.AcquireLockAsync(
            resource,
            effectiveTimeout,
            token);

        return await action();
    }
}
