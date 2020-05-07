using Apache.Geode.DotNetCore;
using GemFireSessionState.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GemFireSessionState.Controllers
{
  public class HomeController : Controller
  {
    private readonly Random rando = new Random();

    private static Region region;
    private static Cache gemfireCache;
    private readonly List<string> sampleData = new List<string> { "Apples", "Apricots", "Avacados", "Bananas", "Blueberries", "Lemons", "Limes", "Mangos", "Oranges", "Pears", "Pineapples" };
    private static readonly string _regionName = "SteeltoeDemo";

    public HomeController(PoolFactory poolFactory, Cache cache)
    {
      Console.WriteLine("HomeController constructor");
      if (region == null)
      {
        Console.WriteLine("Initializing stuff");
        InitializeGemFireObjects(poolFactory, cache);
      }
      Console.WriteLine("Leaving HomeController constructor");
    }

    public IActionResult Index()
    {
      return View();
    }

    public ActionResult GetCacheEntry()
    {
      Console.WriteLine("GetCacheEntry");
      string message;
      try
      {
        //var cacheRegion = gemfireCache.GetRegion<string, string>(_regionName);
        Console.WriteLine("Get from CacheRegion");
        Byte [] byteArray = region.GetByteArray("BestFruit");
        message = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
        Console.WriteLine("Got from CacheRegion {0}", message);
      }
      catch (Exception ex) { message = null; };
      //catch (CacheServerException)
      //{
      //  message = "The region SteeltoeDemo has not been initialized in Gemfire.\r\nConnect to Gemfire with gfsh and run 'create region --name=SteeltoeDemo --type=PARTITION'";
      //}
      //catch (Apache.Geode.Client.KeyNotFoundException)
      //{
      //  message = "Cache has not been set yet.";
      //}

      ViewBag.Message = message;

      return View();
    }

    public ActionResult SetCacheEntry()
    {
      var bestfruit = sampleData.OrderBy(g => Guid.NewGuid()).First();

      region.PutByteArray("BestFruit",
        Encoding.UTF8.GetBytes($"{bestfruit} are the best fruit. Here's random id:{rando.Next()}"));

      return RedirectToAction("GetCacheEntry");
    }

    public ActionResult Reset()
    {
      //Session.Abandon();
      //Request.Cookies.Clear();
      //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

      //var cacheRegion = gemfireCache.GetRegion<string, string>(_regionName);
      region.Remove("BestFruit");
      //cacheRegion = null;
      return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static void InitializeGemFireObjects(PoolFactory poolFactory, Cache cache)
    {
      Console.WriteLine("Create PoolFactory");
      gemfireCache = cache;
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
      var regionFactory = gemfireCache.CreateRegionFactory(RegionShortcut.Proxy);
      try
      {
        Console.WriteLine("Create CacheRegion");
        region = regionFactory.CreateRegion(_regionName);
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
}
