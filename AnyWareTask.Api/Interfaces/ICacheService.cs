namespace AnyWareTask.Api.Interfaces;

public interface ICacheService
{
    Task <T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan TTL);
    Task<bool> DeleteAsync(string key);
}
