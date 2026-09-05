using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.Abstractions.DistributedLock;

public interface IDistributedLockManager
{
    public Task<IDistributedLockHandle?> TryAcquireLockAsync(
        string resource,
        TimeSpan? timeout = default,
        CancellationToken token = default);

    public Task PerformActionWithLockAsync(
        string resource,
        Func<Task> action,
        TimeSpan? timeout = default,
        CancellationToken token = default);

    public Task<TResult> PerformActionWithLockAsync<TResult>(
        string resource,
        Func<Task<TResult>> action,
        TimeSpan? timeout = default,
        CancellationToken token = default);
}