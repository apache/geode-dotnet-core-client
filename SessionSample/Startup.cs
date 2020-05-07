using GemFireSessionState.Models;
using Apache.Geode.Session;
using Steeltoe.Connector.GemFire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using SessionSample.Middleware;
using Microsoft.Extensions.Caching.Distributed;
using System;
using Apache.Geode.DotNetCore;

namespace SessionSample
{
    #region snippet1
    public class Startup
    {
        CacheFactory cacheFactory;
        Cache cache;
        PoolFactory poolFactory;
        RegionFactory regionFactory;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        public void ConfigureServices(IServiceCollection services)
        {
          services.AddGemFireConnection(Configuration, typeof(BasicAuthInitialize), loggerFactory: LoggerFactory);

          // TODO: Don't hardcode region name here
          services.AddSingleton<IDistributedCache>((isp) => new SessionStateCache(isp.GetRequiredService<Cache>(), "SteeltoeDemo", isp.GetService<ILogger<SessionStateCache>>()));

          services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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

    private void InitializeGemFireObjects()
        {
          Console.WriteLine("Create PoolFactory");
          //gemfireCache.TypeRegistry.PdxSerializer = new ReflectionBasedAutoSerializer();

          try
          {
            Console.WriteLine("Create Pool");
            // make sure the pool has been created
            poolFactory.CreatePool("pool");
          }
          catch (Exception ex) { };
          //catch (IllegalStateException e)
          //{
          //    // we end up here with this message if you've hit the reset link after the pool was created
          //    if (e.Message != "Pool with the same name already exists")
          //    {
          //        throw;
          //    }
          //}

          Console.WriteLine("Create Cache RegionFactory");
          var regionFactory = cache.CreateRegionFactory(RegionShortcut.Proxy);
          try
          {
            Console.WriteLine("Create CacheRegion");
            regionFactory.CreateRegion("SteeltoeDemo");
            Console.WriteLine("CacheRegion created");
          }
          catch (Exception ex) { };
          //catch
          //{
          //    Console.WriteLine("Create CacheRegion failed... now trying to get the region");
          //    cacheRegion = gemfireCache.GetRegion<string, string>(_regionName);
          //}
        }
  }
    #endregion
}
