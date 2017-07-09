using System.Web.Routing;

namespace Tomelt.Mvc {
    public interface IHasRequestContext {
        RequestContext RequestContext { get; }
    }
}