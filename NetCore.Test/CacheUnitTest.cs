using System;
using System.Net.Cache;
using Apache.Geode.NetCore;
using NUnit.Framework;

namespace GemfireDotNetTest
{
    public class CacheUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TestClientCacheGetPdxReadSerialized()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "debug")
                    .SetProperty("log-file", "TestClientCacheGetPdxReadSerialized.log"))
                {
                    try
                    {
                        cacheFactory.PdxReadSerialized = true;
                        using (var cache = cacheFactory.CreateCache())
                        {
                            Assert.AreEqual(cache.GetPdxReadSerialized(), true);
                        }

                        cacheFactory.PdxReadSerialized = false;
                        using (var otherCache = cacheFactory.CreateCache())
                        {
                            Assert.AreEqual(otherCache.GetPdxReadSerialized(), false);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                Assert.Pass();
            }
        }
        
        [Test]
        public void TestClientCacheGetPdxIgnoreUnreadFields()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none")
                    .SetProperty("log-file", "geode_native.log"))
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    using (var cache = cacheFactory.CreateCache())
                    {
                        Assert.AreEqual(cache.GetPdxIgnoreUnreadFields(), true);
                    }

                    cacheFactory.PdxIgnoreUnreadFields = false;
                    using (var otherCache = cacheFactory.CreateCache())
                    {
                        Assert.AreEqual(otherCache.GetPdxIgnoreUnreadFields(), false);
                    }
                }

                Assert.Pass();
            }
        }

        [Test]
        public void TestClientCacheGetPoolManager()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none")
                    .SetProperty("log-file", "geode_native.log"))
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    using (var cache = cacheFactory.CreateCache())
                    {
                        var poolManager = cache.PoolManager;
                    }
                }

                Assert.Pass();
            }
        }
        
        [Test]
        public void TestClientCacheCreateRegionFactory()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none")
                    .SetProperty("log-file", "geode_native.log"))
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    using (var cache = cacheFactory.CreateCache())
                    {
                        using (var regionFactory = cache.CreateRegionFactory(RegionShortcut.Proxy))
                        {
                            ;
                        }
                    }
                }

                Assert.Pass();
            }
        }

        [Test]
        public void TestClientCacheGetName()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none"))
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    using (var cache = cacheFactory.CreateCache())
                    {
                        var cacheName = cache.Name;
                        Assert.AreNotEqual(cacheName, String.Empty);
                    }
                }

                Assert.Pass();
            }
        }
        
        [Test]
        public void TestClientCacheClose()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none"))
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    using (var cache = cacheFactory.CreateCache())
                    {
                        Assert.IsFalse(cache.Closed);
                        Assert.DoesNotThrow(delegate { cache.Close(); });
                        Assert.IsTrue(cache.Closed);
                    }
                }

                Assert.Pass();
            }
        }
        
        [Test]
        public void TestClientCacheCloseWithKeepalive()
        {
            using (var client = new Client())
            {
                using (var cacheFactory = CacheFactory.Create()
                    .SetProperty("log-level", "none"))
                {
                    cacheFactory.PdxIgnoreUnreadFields = true;
                    using (var cache = cacheFactory.CreateCache())
                    {
                        Assert.IsFalse(cache.Closed);
                        Assert.DoesNotThrow(delegate() { cache.Close(true); });
                        Assert.IsTrue(cache.Closed);

                    }

                    using (var otherCache = cacheFactory.CreateCache())
                    {
                        Assert.IsFalse(otherCache.Closed);
                        Assert.DoesNotThrow(delegate() { otherCache.Close(false); });
                        Assert.IsTrue(otherCache.Closed);
                    }
                }

                Assert.Pass();
            }
        }
    }
}

