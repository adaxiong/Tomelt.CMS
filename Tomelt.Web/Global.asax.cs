using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Tomelt.Environment;
using Tomelt.WarmupStarter;

namespace Tomelt.Web
{
    public class Global : HttpApplication
    {
        private static Starter<ITomeltHost> _starter;
        protected void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            _starter = new Starter<ITomeltHost>(HostInitialization, HostBeginRequest, HostEndRequest);
            _starter.OnApplicationStart(this);
        }
        protected void Application_BeginRequest()
        {
            _starter.OnBeginRequest(this);
        }

        protected void Application_EndRequest()
        {
            _starter.OnEndRequest(this);
        }

        private static void HostBeginRequest(HttpApplication application, ITomeltHost host)
        {
            application.Context.Items["originalHttpContext"] = application.Context;
            host.BeginRequest();
        }

        private static void HostEndRequest(HttpApplication application, ITomeltHost host)
        {
            host.EndRequest();
        }

        private static ITomeltHost HostInitialization(HttpApplication application)
        {
            var host = TomeltStarter.CreateHost(MvcSingletons);

            host.Initialize();

            // initialize shells to speed up the first dynamic query
            host.BeginRequest();
            host.EndRequest();

            return host;
        }

        static void MvcSingletons(ContainerBuilder builder)
        {
            builder.Register(ctx => RouteTable.Routes).SingleInstance();
            builder.Register(ctx => ModelBinders.Binders).SingleInstance();
            builder.Register(ctx => ViewEngines.Engines).SingleInstance();
        }
    }
}