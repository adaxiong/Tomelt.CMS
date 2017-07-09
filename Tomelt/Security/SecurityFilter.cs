using System.Linq;
using System.Web.Mvc;
using Tomelt.Logging;
using Tomelt.Mvc.Filters;
using Tomelt.UI.Admin;

namespace Tomelt.Security {
    public class SecurityFilter : FilterProvider, IExceptionFilter, IAuthorizationFilter {
        private readonly IAuthorizer _authorizer;

        public SecurityFilter(IAuthorizer authorizer) {
            _authorizer = authorizer;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext) {

            var accessFrontEnd = filterContext.ActionDescriptor.GetCustomAttributes(typeof (AlwaysAccessibleAttribute), true).Any();

            if (!accessFrontEnd && filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), true).Any()) {
                accessFrontEnd = true;
            }

            if (!AdminFilter.IsApplied(filterContext.RequestContext) && !accessFrontEnd && !_authorizer.Authorize(StandardPermissions.AccessFrontEnd)) {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnException(ExceptionContext filterContext) {
            if (!(filterContext.Exception is TomeltSecurityException))
                return;

            try {
                Logger.Information(filterContext.Exception, "Security exception converted to access denied result");
            }
            catch {
                //a logger exception can't be allowed to interrupt this process
            }

            filterContext.Result = new HttpUnauthorizedResult();
            filterContext.ExceptionHandled = true;
        }
    }
}
