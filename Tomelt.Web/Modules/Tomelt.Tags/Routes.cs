using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt.Mvc.Routes;

namespace Tomelt.Tags {
    public class Routes : IRouteProvider {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Priority = 5,
                    Route = new Route(
                        "Tags/{tagName}",
                        new RouteValueDictionary {
                            {"area", "Tomelt.Tags"},
                            {"controller", "Home"},
                            {"action", "Search"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Tomelt.Tags"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}