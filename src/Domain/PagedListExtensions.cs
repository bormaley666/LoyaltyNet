using System;
using System.Linq;

namespace LoyaltyNet.Domain;

public static class PagedListExtensions
{
    public static IPagedList<TDestination> Map<TSource, TDestination>(
        this IPagedList<TSource> pagedList,
        Func<TSource, TDestination> mapper)
    {
        ArgumentNullException.ThrowIfNull(pagedList);
        ArgumentNullException.ThrowIfNull(pagedList.Items);

        var items = pagedList.Items.Select(mapper).ToArray();

        return new PagedList<TDestination>(items, pagedList.PageIndex, pagedList.PageSize, pagedList.TotalItems);
    }
}
