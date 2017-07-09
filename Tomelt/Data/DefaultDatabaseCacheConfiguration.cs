using NHibernate.Cfg.Loquacious;

namespace Tomelt.Data {
    public class DefaultDatabaseCacheConfiguration : IDatabaseCacheConfiguration {
        public void Configure(ICacheConfigurationProperties cache) {
            cache.UseQueryCache = false;
        }
    }
}