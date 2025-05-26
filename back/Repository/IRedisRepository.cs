namespace backapi.Repository
{
    public interface IRedisRepository
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null);
        Task<bool> DeleteKeyAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    }
}
