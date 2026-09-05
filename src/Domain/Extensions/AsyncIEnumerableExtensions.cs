using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoyaltyNet.Domain.Extensions;

namespace LoyaltyNet.Domain.Extensions;

public static class AsyncIEnumerableExtensions
{
    public static IAsyncEnumerable<TResult> SelectAwait<TSource, TResult>(this IEnumerable<TSource> source,
        Func<TSource, ValueTask<TResult>> predicate)
    {
        return source.ToAsyncEnumerable().SelectAwait(predicate);
    }
}
