using System.Text.Json;
using StackExchange.Redis;

namespace PromoCode.Infrastructure.Repositories;

/// <summary>
/// A class representing a Redis cache repository.
/// Implements the ICacheRepository interface.
/// </summary>
public class RedisCacheRepository : ICacheRepository
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisCacheRepository"/> class.
    /// </summary>
    /// <param name="redis">The Redis connection multiplexer.</param>
    public RedisCacheRepository(IConnectionMultiplexer redis)
    {
        _redis = redis?? throw new ArgumentNullException(nameof(redis));
        _database = _redis.GetDatabase();
    }

    /// <summary>
    /// Retrieves the value associated with the specified key from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The key of the value.</param>
    /// <returns>The value associated with the key, or default(T) if the key does not exist.</returns>
    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty)
        {
            return default;
        }
        return JsonSerializer.Deserialize<T>(value!);
    }

    /// <summary>
    /// Sets the value associated with the specified key in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The key of the value.</param>
    /// <param name="value">The value to be set.</param>
    /// <param name="expiry">The optional expiry time for the value.</param>
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serializedValue, expiry);
    }

    /// <summary>
    /// Removes the value associated with the specified key from the cache.
    /// </summary>
    /// <param name="key">The key of the value to be removed.</param>
    /// <returns>True if the key was found and removed; otherwise, false.</returns>
    public async Task<bool> RemoveAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    /// <summary>
    /// Checks if the specified key exists in the cache.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the key exists; otherwise, false.</returns>
    public async Task<bool> ExistsAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }
}