namespace PromoCode.Application.Services;

public interface ICacheService
{
    Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> failover, TimeSpan? expiry = null);
    
    Task<bool> RemoveAsync(string key);
    
    Task<bool> ExistsAsync(string key);
}