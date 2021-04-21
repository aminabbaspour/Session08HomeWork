using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheAdapter.Infra
{
    public interface ICacheAdapter
    {
        TOutput Get<TOutput>(string key);
        void Set<TInput>(string key, TInput input);
        void Set<TInput>(string key, TInput input, CacheOptions options);
        void Remove(string key);
    }
}
