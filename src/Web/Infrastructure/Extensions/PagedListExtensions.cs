using System;
using System.Linq;
using LoyaltyNet.Domain;
using LoyaltyNet.Web.Infrastructure;

namespace LoyaltyNet.Web.Infrastructure.Extensions;

public static class PagedListExtensions
{
    public static PagedListResponse<TDestination> ToResponse<TSource, TDestination>(this IPagedList<TSource> pagedList, Func<TSource, TDestination> mapper)
    {
        ArgumentNullException.ThrowIfNull(pagedList);
        ArgumentNullException.ThrowIfNull(mapper);

        return new PagedListResponse<TDestination>()
        {
            Items = [.. pagedList.Items.Select(mapper)],
            PageIndex = pagedList.PageIndex,
            PageSize = pagedList.PageSize,
            TotalItems = pagedList.TotalItems,
            TotalPages = pagedList.TotalPages
        };
    }
}
