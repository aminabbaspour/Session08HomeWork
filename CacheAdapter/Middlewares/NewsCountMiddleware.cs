using CacheAdapter.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CacheAdapter.Middlewares
{
    public class NewsCountMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NewsRepository _newsRepository;
        private readonly ICacheAdapter _cacheAdapter;

        public NewsCountMiddleware(RequestDelegate next, NewsRepository newsRepository, ICacheAdapter cacheAdapter)
        {
            _next = next;
            _newsRepository = newsRepository;
            _cacheAdapter = cacheAdapter;
        }
        public async Task Invoke(HttpContext context)
        {
            var newsCount = _cacheAdapter.Get<string>("NewsCount");
            if (string.IsNullOrEmpty(newsCount))
            {
                newsCount = _newsRepository.GetNewCount().ToString();
                //_cacheAdapter.Set("NewsCount", newsCount);
                _cacheAdapter.Set("NewsCount", newsCount, new CacheOptions { 
                    SlidingExpiration = TimeSpan.FromSeconds(3),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                });
            }

            await context.Response.WriteAsync($"News Count is {newsCount} \n");
            await _next(context);
        }
    }
}
