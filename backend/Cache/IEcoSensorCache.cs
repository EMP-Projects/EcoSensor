namespace EcoSensorApi.Cache;

/// <summary>
/// Interface for managing the cache of EcoSensor data.
/// </summary>
/// <typeparam name="T">The type of objects to be cached, which must be a class.</typeparam>
public interface IEcoSensorCache<T> where T : class
{
    /// <summary>
    /// Retrieves the list of cached objects.
    /// </summary>
    /// <returns>A list of cached objects if present; otherwise, null.</returns>
    List<T> Get();

    /// <summary>
    /// Caches the provided list of objects.
    /// </summary>
    /// <param name="list">The list of objects to cache.</param>
    /// <param name="cacheDuration">The duration for which the cache should be valid. If null, a default duration is used.</param>
    void Set(List<T> list, TimeSpan? cacheDuration);

    /// <summary>
    /// Removes the cached objects.
    /// </summary>
    void Remove();

    /// <summary>
    /// Initializes the cache with necessary data.
    /// </summary>
    /// <returns>A task that represents the asynchronous cache initialization operation.</returns>
    Task CacheInit();
}