using System.Web;

namespace Tomelt.Mvc {
    public interface IHttpContextAccessor {
        HttpContextBase Current();
        void Set(HttpContextBase httpContext);
    }
}
