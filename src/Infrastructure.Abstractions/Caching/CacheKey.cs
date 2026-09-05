using System;

namespace LoyaltyNet.Infrastructure.Abstractions.Caching;

/// <summary>
/// Ключ кэша, содержащий составную информацию о ключе и параметрах хранения
/// </summary>
public readonly struct CacheKey
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="key">Строковое представление ключа</param>
    public CacheKey(string key)
        : this(key, null, []) { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="key">Строковое представление ключа</param>
    /// <param name="cacheLifeTime">Продолжительность хранения записи в кэше</param>
    public CacheKey(string key, TimeSpan? cacheLifeTime)
        : this(key, cacheLifeTime, []) { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="key">Строковое представление ключа</param>
    /// <param name="prefixes">Префиксы ключа</param>
    public CacheKey(string key, params string[] prefixes)
        : this(key, null, prefixes) { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="key">Строковое представление ключа</param>
    /// <param name="cacheLifeTime">Продолжительность хранения записи в кэше</param>
    /// <param name="prefixes">Префиксы ключа</param>
    public CacheKey(string key, TimeSpan? cacheLifeTime, params string[] prefixes)
    {
        Key = key;
        CacheLifeTime = cacheLifeTime;
        Prefixes = prefixes ?? [];
    }

    /// <summary>
    /// Строковое представление ключа
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Продолжительность хранения записи в кэше
    /// </summary>
    public TimeSpan? CacheLifeTime { get; }

    /// <summary>
    /// Префиксы ключа
    /// </summary>
    public string[] Prefixes { get; }
}
