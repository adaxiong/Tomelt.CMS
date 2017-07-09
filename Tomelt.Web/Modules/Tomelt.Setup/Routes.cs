using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt.Mvc.Routes;

namespace Tomelt.Setup {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "{controller}/{action}",
                        new RouteValueDictionary {
                            {"area", "Tomelt.Setup"},
                            {"controller", "Setup"},
                            {"action", "Index"}
                        },
                        new RouteValueDictionary {
                            {"area", "Tomelt.Setup"},
                            {"controller", "Setup"},
                        },
                        new RouteValueDictionary {
                            {"area", "Tomelt.Setup"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}