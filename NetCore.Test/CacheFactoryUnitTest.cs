using System;
using Apache.Geode.NetCore;
using Xunit;

namespace GemfireDotNetFact
{
    public class CacheFactoryUnitFacts 
    {
        [Fact]
        public void FactCreateFactory()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    Assert.NotNull(cacheFactory);
                }
            }
        }
        
        [Fact]
        public void FactCacheFactoryGetVersion()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    var version = cacheFactory.Version;
                    Assert.NotEqual(version, String.Empty);
                }
            }
        }
        
        [Fact]
        public void FactCacheFactoryGetProductDescription()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    var description = cacheFactory.ProductDescription;
                    Assert.NotEqual(description, String.Empty);
                }
            }
        }
        
        [Fact]
        public void FactCacheFactorySetPdxIgnoreUnreadFields()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    cacheFactory.PdxIgnoreUnreadFields = false;
                }
            }
        }
        
        [Fact]
        public void FactCacheFactorySetPdxReadSerialized()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    cacheFactory.PdxReadSerialized = true;
                    cacheFactory.PdxReadSerialized = false;
                }
            }
        }
        
        [Fact]
        public void FactCacheFactoryCreateCache()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    using (var cache = cacheFactory.CreateCache())
                    {
                        ;
                    }
                }
            }
        }
        
        [Fact]
        public void FactCacheFactorySetProperty()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create())
                {
                    cacheFactory.SetProperty("log-level", "none")
                        .SetProperty("log-file", "geode_native.log");
                }
            }
        }
    }
}