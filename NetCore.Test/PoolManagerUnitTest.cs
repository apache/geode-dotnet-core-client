using System.Net.Cache;
using Apache.Geode.NetCore;
using NUnit.Framework;

namespace GemfireDotNetTest
{
    public class PoolManagerUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TestPoolManagerCreatePoolFactory()
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
                            ;
                        }
                    }
                }
            }
            Assert.Pass();
        }
    }
}

