using System;

namespace LoyaltyNet.Infrastructure.Caching;

/// <summary>
/// Параметры конфигурации кэширования
/// </summary>
public sealed class CacheOptions
{
    private const int DefaultCacheSizeLimit = 10000;
    private static readonly TimeSpan DefaultCacheLifeTime = TimeSpan.FromMinutes(30);

    private int? _cacheSizeLimit;
    private TimeSpan? _cacheLifetime;

    /// <summary>
    /// Срок действия кэша
    /// </summary>
    public TimeSpan CacheLifetime 
    {
        get => _cacheLifetime ?? DefaultCacheLifeTime;
        set => _cacheLifetime = value;
    }

    /// <summary>
    /// Лимит кэша
    /// </summary>
    public int CacheSizeLimit 
    {
        get => _cacheSizeLimit ?? DefaultCacheSizeLimit;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "must not be less than zero");
            }

            _cacheSizeLimit = value;
        }
    }
}
