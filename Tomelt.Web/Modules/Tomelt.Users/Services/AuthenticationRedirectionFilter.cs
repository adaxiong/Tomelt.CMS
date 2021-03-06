﻿using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using Tomelt.Mvc.Filters;

namespace Tomelt.Users.Services {

    /// <summary>
    /// This class is responsible for redirecting the user to the authentication page 
    /// of the current tenant.
    /// </summary>
    public class AuthenticationRedirectionFilter : FilterProvider, IAuthenticationFilter {

        public void OnAuthentication(AuthenticationContext filterContext) {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) {
            if (filterContext.Result is HttpUnauthorizedResult) {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "AccessDenied" },
                    { "area", "Tomelt.Users"},
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                });
            }
        }
    }
}