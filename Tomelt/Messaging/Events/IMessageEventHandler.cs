using System;
using Tomelt.Events;
using Tomelt.Messaging.Models;

namespace Tomelt.Messaging.Events {
    [Obsolete]
    public interface IMessageEventHandler : IEventHandler {
        void Sending(MessageContext context);
        void Sent(MessageContext context);
    }
}
