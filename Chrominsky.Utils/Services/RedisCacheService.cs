using Chrominsky.Utils.Repositories;

namespace Chrominsky.Utils.Services;

/// <summary>
/// Implements the caching service using Redis as the storage backend.
/// </summary>
public class RedisCacheService : ICacheService
{
    private readonly ICacheRepository _cacheRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisCacheService"/> class.
    /// </summary>
    /// <param name="cacheRepository">The cache repository.</param>
    public RedisCacheService(ICacheRepository cacheRepository)
    {
        _cacheRepository = cacheRepository;
    }

    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// If the key does not exist or the value is expired, the provided failover function is invoked to retrieve a new value.
    /// The new value is then stored in the cache with an optional expiry time.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="failover">The function to invoke to retrieve a new value if the key does not exist or is expired.</param>
    /// <param name="expiry">The optional expiry time for the cached value.</param>
    /// <returns>The cached value or the result of the failover function.</returns>
    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> failover, TimeSpan? expiry = null)
    {
        var value = await _cacheRepository.GetAsync<T>(key);
        if (value == null || value.Equals(default(T)))
        {
            value = await failover();
            if (value!= null &&!value.Equals(default(T)))
            {
                await _cacheRepository.SetAsync(key, value, expiry);
            }
        }
        return value;
    }

    /// <summary>
    /// Removes the specified key from the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <returns>A task that completes when the key has been removed from the cache.</returns>
    public async Task<bool> RemoveAsync(string key)
    {
        return await _cacheRepository.RemoveAsync(key);
    }

    /// <summary>
    /// Checks if the specified key exists in the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <returns>A task that completes with a boolean indicating whether the key exists in the cache.</returns>
    public async Task<bool> ExistsAsync(string key)
    {
        return await _cacheRepository.ExistsAsync(key);
    }
}