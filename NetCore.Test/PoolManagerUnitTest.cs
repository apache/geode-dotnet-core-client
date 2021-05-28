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
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    using (var cache = cacheFactory.CreateCache())
                    {
                        using (var poolManager = cache.PoolManager)
                        {
                            using (var poolFactory = poolManager.CreatePoolFactory())
                            {
                            }
                        }
                    }
                }
            }
            Assert.Pass();
        }
    }
}

