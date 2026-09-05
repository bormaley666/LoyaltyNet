using System;
using System.Collections.Concurrent;
using System.Threading;
using LoyaltyNet.Infrastructure.Abstractions.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace LoyaltyNet.Infrastructure.Caching;

/// <inheritdoc/>
internal sealed class CacheManager : ICacheManager
{
    private ConcurrentDictionary<string, CancellationTokenSource> _prefixTokens;
    private readonly CacheOptions _options;
    private readonly MemoryCache _cache;

    /// <summary>
    /// ctor
    /// </summary>
    public CacheManager(CacheOptions options)
    {
        _prefixTokens = new ConcurrentDictionary<string, CancellationTokenSource>();
        _options = options;
        _cache = new MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = options.CacheSizeLimit
        });
    }

    /// <inheritdoc/>
    public bool TryGet<T>(string key, out T? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        return _cache.TryGetValue(key, out value);
    }

    /// <inheritdoc/>
    public bool Add(CacheKey cacheKey, object value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (!_cache.TryGetValue(cacheKey.Key, out var _))
        {
            var options = CreateEntryOptions(cacheKey.CacheLifeTime ?? _options.CacheLifetime);

            AddPrefixTokens(options, cacheKey.Prefixes);

            _cache.Set(cacheKey.Key, value, options);

            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public void Put(CacheKey cacheKey, object value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var options = CreateEntryOptions(cacheKey.CacheLifeTime ?? _options.CacheLifetime);

        AddPrefixTokens(options, cacheKey.Prefixes);

        _cache.Set(cacheKey.Key, value, options);
    }

    /// <inheritdoc/>
    public void Remove(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        _cache.Remove(key);
    }

    /// <inheritdoc/>
    public void RemoveByPrefix(string prefix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prefix);

        _prefixTokens.TryRemove(prefix, out var tokenSource);

        tokenSource?.Cancel();
        tokenSource?.Dispose();
    }

    /// <inheritdoc/>
    public void Clear()
    {
        _prefixTokens = new ConcurrentDictionary<string, CancellationTokenSource>();
        _cache.Clear();
    }

    private static MemoryCacheEntryOptions CreateEntryOptions(TimeSpan cacheLifeTime) =>
        new()
        {
            AbsoluteExpirationRelativeToNow = cacheLifeTime,
            Size = 1
        };

    private void AddPrefixTokens(MemoryCacheEntryOptions options, params string[] prefixes)
    {
        foreach (var prefix in prefixes)
        {
            var tokenSource = _prefixTokens.GetOrAdd(prefix, new CancellationTokenSource());
            options.AddExpirationToken(new CancellationChangeToken(tokenSource.Token));
        }
    }
}
