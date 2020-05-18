using System;
using Xunit;
using Apache.Geode.NetCore;
using Apache.Geode.Session;
using System.IO;

namespace Apache.Geode.Session.Tests
{
  [Trait("Category", "Integration")]
  public class SessionStateStoreTests : IDisposable
  {
    private static string _regionName = "testRegion";

    private Cache Cache { get; set; }
    private string CacheXmlFilePath { get; set; }
    private string GeodePropertiesFilePath { get; set; }
    private const string CacheXmlFileNameKey = "cache-xml-file";

    public SessionStateStoreTests()
    {
      //SessionStateCache.IsInitialized = false;
    }

    public void Dispose()
    {
      try
      {
        Cache.Close();
      }
      catch (Exception)
      {
      }
    }

    [Fact]
    public void NullPoolFactory_Throws()
    {
      PoolFactory poolFactory = null;
      var cacheFactory = CacheFactory.Create()
          .SetProperty("log-level", "debug")
          .SetProperty("log-file", "SessionStateCacheTests.log");

      var cache = (Cache)cacheFactory.CreateCache();
      Assert.Throws<ArgumentNullException>(() => new SessionStateCache(cache, _regionName));
    }
  }
}
