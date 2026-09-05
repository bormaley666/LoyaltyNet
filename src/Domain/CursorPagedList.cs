using System;
using System.Collections.Generic;

namespace LoyaltyNet.Domain;

/// <inheritdoc/>
public sealed class CursorPagedList<T> : ICursorPagedList<T>
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="items"></param>
    /// <param name="nextPageToken"></param>
    public CursorPagedList(IList<T> items, string? nextPageToken)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        NextPageToken = nextPageToken;
    }

    /// <inheritdoc/>
    public string? NextPageToken { get; }

    /// <inheritdoc/>
    public IList<T> Items { get; }
}
