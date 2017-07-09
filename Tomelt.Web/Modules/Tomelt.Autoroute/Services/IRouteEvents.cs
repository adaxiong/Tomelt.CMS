using System;
using Tomelt.ContentManagement;
using Tomelt.Events;

namespace Tomelt.Autoroute.Services {
    public interface IRouteEvents : IEventHandler {
        void Routed(IContent content, String path);
    }
}