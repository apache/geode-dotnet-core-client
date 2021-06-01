using System.Net.Cache;
using Apache.Geode.NetCore;
using Xunit;

namespace GemfireDotNetTest
{
    public class PoolFactoryUnitTests
    {
        [Fact]
        public void TestPoolFactoryAddLocator()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                        .SetProperty("log-level", "none")
                        .SetProperty("log-file", "geode_native.log"))
                {
                    using (var cache = cacheFactory.CreateCache())
                    {
                        using (var poolManager = cache.PoolManager)
                        {
                            using (var poolFactory = poolManager.CreatePoolFactory())
                            {
                              poolFactory.AddLocator("localhost", 10334);
                            }
                        }
                    }
                }
            }
        }
        
        [Fact]
        public void TestPoolFactoryCreatePool()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                        .SetProperty("log-level", "none")
                        .SetProperty("log-file", "geode_native.log"))
                {
                    using (var cache = cacheFactory.CreateCache())
                    {
                        using (var poolManager = cache.PoolManager)
                        {
                            using (var poolFactory = poolManager.CreatePoolFactory())
                            {
                                poolFactory.AddLocator("localhost", 10334);
                                using (var pool = poolFactory.CreatePool("myPool"))
                                {
                                    ;
                                }
                            }
                        }
                    }
                }
            }
        }

        [Fact]
        public void TestCreatePoolWithoutPoolManager()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    using (var cache = cacheFactory.CreateCache())
                    {
                        using (var poolFactory = cache.PoolFactory)
                        {
                            poolFactory.AddLocator("localhost", 10334);
                            using (var pool = poolFactory.CreatePool("myPool"))
                            {
                            }
                        }
                    }
                }
            }
        }
  }
}

