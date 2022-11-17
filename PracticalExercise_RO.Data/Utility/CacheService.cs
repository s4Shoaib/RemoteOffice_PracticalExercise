using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Caching;

namespace PracticalExercise_RO.Data.Utility
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Save<T>(string key, T value, DateTimeOffset offset);
        void Remove(string key);
    }

    [ExcludeFromCodeCoverage]
    public class CacheService : ICacheService
    {
        public T Get<T>(string key)
        {
            var cacheValue = MemoryCache.Default[key];

            if (cacheValue == null) return default(T);
            if (cacheValue is T)
            {
                return (T)cacheValue;
            }
            else
            {
                throw new InvalidOperationException("Invalid cache data.");
            }
        }

        public void Save<T>(string key, T value, DateTimeOffset offset)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = offset };
            var cache = MemoryCache.Default;
            cache.Set(key, value, policy);
        }

        public void Remove(string key)
        {
            var cache = MemoryCache.Default;
            cache.Remove(key);
        }
    }
}
