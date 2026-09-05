using System;

namespace LoyaltyNet.Domain;

/// <inheritdoc/>
internal sealed class TimeProvider : ITimeProvider
{
    /// <inheritdoc/>
    public DateTime GetLocalNow() => DateTime.Now;

    /// <inheritdoc/>
    public DateTime GetLocalNowAsUtcKind()
    {
        var localTime = DateTime.Now;

        return DateTime.SpecifyKind(localTime, DateTimeKind.Utc);
    }

    /// <inheritdoc/>
    public DateTime GetUtcNow() => DateTime.UtcNow;
}
