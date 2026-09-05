using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using LoyaltyNet.Domain;

namespace LoyaltyNet.Persistence.Extensions;

internal static class IQueryableExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken token = default)
    {
        if (source == null)
        {
            return new PagedList<T>([], pageIndex, pageSize);
        }

        pageSize = Math.Max(pageSize, 1);

        var count = await source.CountAsync(token);
        var items = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(token);

        return new PagedList<T>(items, pageIndex, pageSize, count);
    }
}
