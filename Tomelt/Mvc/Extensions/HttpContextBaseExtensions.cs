using System.Web;

namespace Tomelt.Mvc.Extensions {
    public static class HttpContextBaseExtensions {
        public static bool IsBackgroundContext(this HttpContextBase httpContextBase) {
            return httpContextBase == null || httpContextBase is MvcModule.HttpContextPlaceholder;
        }
    }
}
