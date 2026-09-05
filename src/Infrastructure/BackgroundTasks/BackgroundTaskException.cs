using System;

namespace LoyaltyNet.Infrastructure.BackgroundTasks;

public sealed class BackgroundTaskException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="message"></param>
    public BackgroundTaskException(string message)
        : base(message) { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public BackgroundTaskException(string message, Exception innerException)
        : base(message, innerException) { }
}
