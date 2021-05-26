using System;
using Apache.Geode.NetCore;
using NUnit.Framework;

namespace GemfireDotNetTest
{
    public class ObjectLeakUnitTests 
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLeakCacheFactory()
        {
            var client = new Client();
            
            using (var cacheFactory = CacheFactory.Create(client))
            {
                Assert.Throws<InvalidOperationException>(() => client.Dispose());
            }
        }
    }
}