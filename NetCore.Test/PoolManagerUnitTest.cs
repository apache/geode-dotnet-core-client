using System.Net.Cache;
using Apache.Geode.NetCore;
using Xunit;

namespace GemfireDotNetTest
{
    public class PoolManagerUnitTests
    {
        [Fact]
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
        }
    }
}

