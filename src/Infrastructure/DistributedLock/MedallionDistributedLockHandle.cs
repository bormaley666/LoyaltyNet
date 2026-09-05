using LoyaltyNet.Infrastructure.Abstractions.DistributedLock;
using Medallion.Threading;
using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.DistributedLock;

/// <inheritdoc/>
internal sealed class MedallionDistributedLockerHandle : IDistributedLockHandle
{
    public IDistributedSynchronizationHandle Handle { get; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="handle"></param>
    public MedallionDistributedLockerHandle(IDistributedSynchronizationHandle handle)
    {
        Handle = handle;
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        return Handle.DisposeAsync();
    }
}
