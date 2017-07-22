using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt.Mvc.Routes;

namespace UEditor
{
    
    public class Routes : IRouteProvider {
        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {     
                
                new RouteDescriptor {
                    Route = new Route("ueditor/config", 
                        new RouteValueDictionary {
                            {"area", "UEditor"},
                            {"controller", "UEditor"},
                            {"action", "Config"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "UEditor"}
                        }, new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Route = new Route("ueditor/uploadfile", 
                        new RouteValueDictionary {
                            {"area", "UEditor"},
                            {"controller", "UEditor"},
                            {"action", "UploadFile"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "UEditor"}
                        }, new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Route = new Route("ueditor/getfilelist", 
                        new RouteValueDictionary {
                            {"area", "UEditor"},
                            {"controller", "UEditor"},
                            {"action", "GetFileList"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "UEditor"}
                        }, new MvcRouteHandler())
                },      
                new RouteDescriptor {
                    Route = new Route("ueditor/catchimage", 
                        new RouteValueDictionary {
                            {"area", "UEditor"},
                            {"controller", "UEditor"},
                            {"action", "CatchImage"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "UEditor"}
                        }, new MvcRouteHandler())
                },    
                new RouteDescriptor {
                    Route = new Route("ueditor/test", 
                        new RouteValueDictionary {
                            {"area", "UEditor"},
                            {"controller", "UEditor"},
                            {"action", "Test"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "UEditor"}
                        }, new MvcRouteHandler())
                }  
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var route in GetRoutes()) {
                routes.Add(route);
            }
        }
    }
}