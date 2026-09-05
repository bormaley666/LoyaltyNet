using System;
using System.Threading.Tasks;

namespace LoyaltyNet.Infrastructure.Abstractions.Caching;

public static class CacheManagerExtensions
{
    public static async Task<T> GetOrAddAsync<T>(this ICacheManager cache, CacheKey cacheKey, Func<Task<T>> acquire)
    {
        ArgumentNullException.ThrowIfNull(acquire);

        if (!cache.TryGet<T>(cacheKey.Key, out var value))
        {
            value = await acquire();

            if (value is not null)
            {
                cache.Put(cacheKey, value);
            }
        }

        return value!;
    }
}
