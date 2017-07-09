using System.Collections.Generic;
using Tomelt.Events;

namespace Tomelt.Messaging.Services {
    public interface IMessageService : IEventHandler {
        void Send(string type, IDictionary<string, object> parameters);
    }
}