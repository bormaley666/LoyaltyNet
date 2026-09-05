using System;
using System.Linq;

namespace LoyaltyNet.Domain;

public static class CursorPagedListExtensions
{
    public static ICursorPagedList<TDestination> Map<TSource, TDestination>(
        this ICursorPagedList<TSource> pagedList,
        Func<TSource, TDestination> mapper)
    {
        ArgumentNullException.ThrowIfNull(pagedList);
        ArgumentNullException.ThrowIfNull(pagedList.Items);

        var items = pagedList.Items.Select(mapper).ToArray();

        return new CursorPagedList<TDestination>(items, pagedList.NextPageToken);
    }
}
