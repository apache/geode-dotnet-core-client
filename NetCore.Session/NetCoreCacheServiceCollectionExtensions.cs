using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Apache.Geode.Session
{
    /// <summary>
    /// Extension methods for setting up NetCore distributed cache related services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class SessionStateCacheServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Geode distributed caching services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="setupAction">An <see cref="Action{SessionStateCacheOptions}"/> to configure the provided
        /// <see cref="SessionStateCacheOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddsSessionStateCache(this IServiceCollection services, Action<SessionStateCacheOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            //services.Add(ServiceDescriptor.Singleton<IDistributedCache, SessionStateCache>());

            return services;
        }
    }
}
