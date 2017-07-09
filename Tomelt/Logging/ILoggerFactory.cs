using System;

namespace Tomelt.Logging {
    public interface ILoggerFactory {
        ILogger CreateLogger(Type type);
    }
}