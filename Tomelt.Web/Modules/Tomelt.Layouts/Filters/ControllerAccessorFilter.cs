using System.Web.Mvc;
using Tomelt.Mvc.Filters;

namespace Tomelt.Layouts.Filters {
    public class ControllerAccessorFilter : FilterProvider, IActionFilter {
        public const string CurrentControllerKey = "CurrentController";

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.HttpContext.Items[CurrentControllerKey] = filterContext.Controller;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {}
    }
}