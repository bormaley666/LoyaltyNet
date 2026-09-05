using System;

namespace LoyaltyNet.Infrastructure.DistributedLock;

public sealed class DistributedLockConfiguration
{
    internal string? ConnectionString { get; private set; }

    public DistributedLockConfiguration UsePostgres(string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        ConnectionString = connectionString;

        return this;
    }
}
