using System;
using System.Threading.Tasks;
using Owin;

namespace Tomelt.Owin {
    /// <summary>
    /// A special Owin middleware that is executed last in the Owin pipeline and runs the non-Owin part of the request.
    /// </summary>
    public static class TomeltMiddleware {
        public static IAppBuilder UseTomelt(this IAppBuilder app) {
            app.Use(async (context, next) => {
                var handler = context.Environment["tomelt.Handler"] as Func<Task>;

                if (handler == null) {
                    throw new ArgumentException("tomelt.Handler can't be null");
                }
                await handler();
            });

            return app;
        }
    }
}
