using System;
using Apache.Geode.NetCore;
using Xunit;

namespace GemfireDotNetTest
{
    public class ObjectLeakUnitTests 
    {
        [Fact]
        public void TestLeakCacheFactory()
        {
            var client = new Client();
            
            using (var cacheFactory = CacheFactory.Create())
            {
                Assert.Throws<InvalidOperationException>(() => client.Dispose());
            }
        }
    }
}