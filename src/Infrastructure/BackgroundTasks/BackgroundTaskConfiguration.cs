using System;

namespace LoyaltyNet.Infrastructure.BackgroundTasks;

public sealed class BackgroundTaskConfiguration 
{
    internal string? ConnectionString { get; private set; }

    public BackgroundTaskConfiguration UsePostgres(string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        ConnectionString = connectionString;

        return this;
    }
}
