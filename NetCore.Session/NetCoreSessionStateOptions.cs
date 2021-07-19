using Microsoft.Extensions.Options;

namespace Apache.Geode.Session
{
    /// <summary>
    /// Configuration options for <see cref="SessionStateCache"/>.
    /// </summary>
    public class SessionStateCacheOptions : IOptions<SessionStateCacheOptions>
    {
        /// <summary>
        /// The configuration used to connect to SessionStateCache.
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// The configuration used to connect to SessionStateCache.
        /// This is preferred over Configuration.
        /// </summary>
        //public ConfigurationOptions ConfigurationOptions { get; set; }

        /// <summary>
        /// The Redis instance name.
        /// </summary>
        public string InstanceName { get; set; }

        SessionStateCacheOptions IOptions<SessionStateCacheOptions>.Value
        {
            get { return this; }
        }
    }
}