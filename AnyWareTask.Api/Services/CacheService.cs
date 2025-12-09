using AnyWareTask.Api.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace AnyWareTask.Api.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase database;
    public CacheService(IConnectionMultiplexer connection)
    {
        this.database = connection.GetDatabase();
    }

    async Task<bool> ICacheService.DeleteAsync(string key)
    {
       return await database.KeyDeleteAsync(key);
    }

    async Task<T> ICacheService.GetAsync<T>(string key)
    {
        var value = await database.StringGetAsync(key);
        if (value.IsNullOrEmpty)
        {
            return default;
        }
        var result = JsonSerializer.Deserialize<T>(value);
        return result;
    }

    Task ICacheService.SetAsync<T>(string key, T value, TimeSpan TTL)
    {
       var jsonObj = JsonSerializer.Serialize(value);
       return database.StringSetAsync(key, jsonObj, TTL);
    }
}
