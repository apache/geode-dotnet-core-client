using GemFireSessionState.Models;
using Apache.Geode.Session;
using Steeltoe.Connector.GemFire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Connector;
//using Steeltoe.Management.CloudFoundry;
using Microsoft.Extensions.Caching.Distributed;
using Apache.Geode.DotNetCore;

namespace GemFireSessionState
{
  public class Startup
  {
    public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
      Configuration = configuration;
      LoggerFactory = loggerFactory;
    }

    public IConfiguration Configuration { get; }
    public ILoggerFactory LoggerFactory { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddGemFireConnection(Configuration, typeof(BasicAuthInitialize), loggerFactory: LoggerFactory);
      // TODO: Don't hardcode region name here
      services.AddSingleton<IDistributedCache>((isp) => new SessionStateCache(isp.GetRequiredService<Cache>(), "SteeltoeDemo", isp.GetService<ILogger<SessionStateCache>>()));

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddMvc(option => option.EnableEndpointRouting = false);

      services.AddSession();
      //services.AddSession(options =>
      //{
      //  options.IdleTimeout = System.TimeSpan.FromSeconds(10);
      //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseSession();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
