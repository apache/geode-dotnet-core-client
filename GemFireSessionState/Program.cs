using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
//using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Connector;
using Steeltoe.Extensions.Logging;

namespace GemFireSessionState
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            //.AddCloudFoundry()
            //.UseCloudFoundryHosting()
            .UseStartup<Startup>()
            .ConfigureLogging((context, builder) =>
            {
              builder.AddConfiguration(context.Configuration.GetSection("Logging"));
              builder.AddDynamicConsole();
            });
  }
}
