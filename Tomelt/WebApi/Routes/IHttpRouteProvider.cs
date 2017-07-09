using System.Collections.Generic;
using Tomelt.Mvc.Routes;

namespace Tomelt.WebApi.Routes {
    public interface IHttpRouteProvider : IDependency {
        IEnumerable<RouteDescriptor> GetRoutes();
        void GetRoutes(ICollection<RouteDescriptor> routes);
    }
}
