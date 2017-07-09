using System.Collections.Generic;

namespace Tomelt.Messaging.Services {
    public interface IMessageChannel : IDependency {
        void Process(IDictionary<string, object> parameters);
    }
}