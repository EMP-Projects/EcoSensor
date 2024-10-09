using Microsoft.Extensions.Caching.Memory;

namespace EcoSensorApi.Cache;

/// <inheritdoc />
public abstract class EcoSensorCache<T> : IEcoSensorCache<T> where T : class
{
    private readonly IMemoryCache _cache;
    /// <summary>
    /// Gets or sets the cache key used to identify the cached data.
    /// </summary>
    protected abstract string CacheKey { get; set; }

    /// <summary>
    /// Gets or sets the duration for which the cache entry should be valid.
    /// Default is 1 day.
    /// </summary>
    protected TimeSpan CacheDuration { get; set; } = TimeSpan.FromDays(1);

    /// <summary>
    /// Initializes the cache with necessary data.
    /// </summary>
    /// <returns>A task that represents the asynchronous cache initialization operation.</returns>
    public abstract Task CacheInit();

    /// <summary>
    /// Initializes a new instance of the <see cref="EcoSensorCache{T}"/> class.
    /// </summary>
    /// <param name="memoryCache">The memory cache instance to be used for caching.</param>
    protected EcoSensorCache(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    /// <inheritdoc />
    public List<T> Get()
    {
        _cache.TryGetValue(CacheKey, out List<T>? cachedList);
        return cachedList ?? [];
    }

    /// <inheritdoc />
    public void Set(List<T> list, TimeSpan? cacheDuration)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheDuration ?? CacheDuration
        };
        _cache.Set(CacheKey, list, cacheEntryOptions);
    }

    /// <inheritdoc />
    public void Remove()
    {
        _cache.Remove(CacheKey);
    }
}