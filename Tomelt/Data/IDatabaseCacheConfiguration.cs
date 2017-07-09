using NHibernate.Cfg.Loquacious;

namespace Tomelt.Data {
    public interface IDatabaseCacheConfiguration : IDependency {
        void Configure(ICacheConfigurationProperties cache);
    }
}