using GemFireSessionState.Models;
using Apache.Geode.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using SessionSample.Middleware;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Apache.Geode.NetCore;

namespace SessionSample
{
    #region snippet1
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostContext;

        public Startup(IConfiguration config, IWebHostEnvironment hostContext)
        {
            _config = config;
            _hostContext = hostContext;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddGemFireConnection(Configuration, typeof(BasicAuthInitialize), loggerFactory: LoggerFactory);

            // TODO: Don't hardcode region name here
            //services.AddSingleton<IDistributedCache>((isp) => new SessionStateCache(isp.GetRequiredService<Cache>(), "NetCoreSession", isp.GetService<ILogger<SessionStateCache>>()));
            //services.AddScoped<IDistributedCache, SessionStateCache>();

            services.AddsSessionStateCache(options =>
            {
                //options.IdleTimeout = TimeSpan.FromSeconds(5);
                //options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = true;
                using var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none");

                using var cache = (Cache)cacheFactory.CreateCache();

                using var poolFactory = cache.PoolFactory.AddLocator("localhost", 10334);
                using var pool = poolFactory.CreatePool("myPool");

                using var ssCache = new SessionStateCache(cache, "exampleRegion");
            });
            //services.AddSingleton<IDistributedCache>((isp) => new SessionStateCache(isp.GetRequiredService<Cache>(), "exampleRegion", isp.GetService<ILogger<SessionStateCache>>()));


            services.AddControllersWithViews();
           services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseHttpContextItemsMiddleware();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
    }
  }
    #endregion
}
