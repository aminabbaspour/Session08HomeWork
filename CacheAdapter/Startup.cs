using CacheAdapter.Infra;
using CacheAdapter.Middlewares;
using CacheAdapter.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheAdapter
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var cacheSettings = new CacheSettings();
            _configuration.GetSection(nameof(CacheSettings)).Bind(cacheSettings);

            if (cacheSettings.UseDistributedCache)
            {
                //services.AddDistributedMemoryCache();
                services.AddDistributedSqlServerCache(options =>
                {
                    options.ConnectionString = cacheSettings.ConnectionString;
                    options.SchemaName = cacheSettings.SchemaName;
                    options.TableName = cacheSettings.TableName;
                });
                //services.AddStackExchangeRedisCache(options =>
                //{

                //});
                services.AddSingleton<ICacheAdapter, DistributedCacheAdapter>();

            }
            else
            {
                services.AddMemoryCache();
                services.AddSingleton<ICacheAdapter, MomoryCacheAdapter>();
            }


            services.AddSingleton<NewsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMiddleware<NewsCountMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
