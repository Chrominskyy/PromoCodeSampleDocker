namespace PromoCode.Infrastructure.Repositories;

public interface ICacheRepository
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<bool> RemoveAsync(string key);
    Task<bool> ExistsAsync(string key);
}
