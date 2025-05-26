using backapi.Repository;
using StackExchange.Redis;
using System.Text.Json;

namespace backapi.Services
{
    public class RedisService : IRedisRepository
    {
        private readonly IDatabase _database;

        public RedisService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _database.StringSetAsync(key, value, expiry);
        }

        public async Task<bool> DeleteKeyAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default(T);
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            return await _database.StringSetAsync(key, serializedValue, expiry);
        }
    }
}
