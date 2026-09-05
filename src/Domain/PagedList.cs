using System;
using System.Collections.Generic;
using System.Linq;

namespace LoyaltyNet.Domain;

/// <inheritdoc/>
public sealed class PagedList<T> : IPagedList<T>
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="items">Список элементов</param>
    /// <param name="pageIndex">Индекс страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="totalItems">Общее кол-во записей</param>
    public PagedList(IList<T> items, int pageIndex, int pageSize, int? totalItems = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(pageIndex);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        TotalItems = totalItems ?? items.Count;
        TotalPages = TotalItems / pageSize;

        if (TotalItems % pageSize > 0)
        {
            TotalPages++;
        }

        PageSize = pageSize;
        PageIndex = pageIndex;

        Items = totalItems != null ? items : [.. items.Skip(pageIndex * pageSize).Take(pageSize)];
    }

    /// <inheritdoc/>
    public int PageIndex { get; }

    /// <inheritdoc/>
    public int PageSize { get; }

    /// <inheritdoc/>
    public int TotalPages { get; }

    /// <inheritdoc/>
    public int TotalItems { get; }

    /// <inheritdoc/>
    public IList<T> Items { get; }
}
