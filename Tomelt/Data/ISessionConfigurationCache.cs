using System;
using NHibernate.Cfg;

namespace Tomelt.Data {
    public interface ISessionConfigurationCache {
        Configuration GetConfiguration(Func<Configuration> builder);
    }
}