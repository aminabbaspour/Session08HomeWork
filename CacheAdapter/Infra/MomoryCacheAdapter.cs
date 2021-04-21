using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CacheAdapter.Infra
{
    public class MomoryCacheAdapter : ICacheAdapter
    {
        private readonly IMemoryCache _memoryCache;

        public MomoryCacheAdapter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public TOutput Get<TOutput>(string key)
        {
            var cacheValue = _memoryCache.Get(key);
            TOutput output = default(TOutput);
            if (cacheValue != null)
            {
                var stringResult = cacheValue.ToString();
                output = JsonConvert.DeserializeObject<TOutput>(stringResult);
            }

            return output;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Set<TInput>(string key, TInput input)
        {
            var serializedObject = JsonConvert.SerializeObject(input);

            _memoryCache.Set(key, serializedObject);
        }

        public void Set<TInput>(string key, TInput input, CacheOptions options)
        {
            var serializedObject = JsonConvert.SerializeObject(input);

            //TODO: Use Automapper
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = options.SlidingExpiration
            };

            _memoryCache.Set(key, serializedObject, cacheOptions);
        }
    }
}
