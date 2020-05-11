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
  }
    #endregion
}
