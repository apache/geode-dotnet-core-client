using System;
using Apache.Geode.DotNetCore;
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
            var client = new GemfireClient();
            
            using (var cacheFactory = CacheFactory.Create())
            {
                Assert.Throws<InvalidOperationException>(() => client.Dispose());
            }
        }
    }
}