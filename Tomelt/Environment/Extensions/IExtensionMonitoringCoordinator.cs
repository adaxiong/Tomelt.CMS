using System;
using Tomelt.Caching;

namespace Tomelt.Environment.Extensions {
    public interface IExtensionMonitoringCoordinator {
        void MonitorExtensions(Action<IVolatileToken> monitor);
    }
}