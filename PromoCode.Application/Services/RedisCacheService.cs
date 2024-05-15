using PromoCode.Infrastructure.Repositories;

namespace PromoCode.Application.Services;

public class RedisCacheService : ICacheService
{
    private readonly ICacheRepository _cacheRepository;

    public RedisCacheService(ICacheRepository cacheRepository)
    {
        _cacheRepository = cacheRepository;
    }
    
    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> failover, TimeSpan? expiry = null)
    {
        var value = await _cacheRepository.GetAsync<T>(key);
        if (value == null || value.Equals(default(T)))
        {
            value = await failover();
            if (value != null && !value.Equals(default(T)))
            {
                await _cacheRepository.SetAsync(key, value, expiry);
            }
        }
        return value;
    }

    public async Task<bool> RemoveAsync(string key)
    {
        return await _cacheRepository.RemoveAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _cacheRepository.ExistsAsync(key);
    }
}