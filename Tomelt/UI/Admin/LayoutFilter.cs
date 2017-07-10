using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt;
using Tomelt.Mvc.Filters;

namespace Tomelt.UI.Admin
{
    public class LayoutFilter : FilterProvider, IResultFilter
    {
        private readonly IWorkContextAccessor _wca;

        public LayoutFilter(IWorkContextAccessor wca)
        {
            _wca = wca;
        }
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var workContext = _wca.GetContext();
            var area = filterContext.RouteData.Values["area"].ToString().ToLower();
            if (area!= "dashboard")
            {
                //workContext.Layout.Metadata.Alternates.Add("Layout__");
                workContext.Layout.Metadata.Alternates.Add("Layout__Tab");
            }
            
            //workContext.Layout.Metadata.Alternates.Add(
            //    BuildShapeName(routeValues, "area"));
            //workContext.Layout.Metadata.Alternates.Add(
            //    BuildShapeName(routeValues, "area", "controller"));
            //workContext.Layout.Metadata.Alternates.Add(
            //    BuildShapeName(routeValues, "area", "controller", "action"));
        }
        //private static string BuildShapeName(
        //    RouteValueDictionary values, params string[] names) {

        //    return "Layout__" +
        //        string.Join("__",
        //            names.Select(s =>
        //                ((string)values[s] ?? "").Replace(".", "_")));
        //}
    }
}
