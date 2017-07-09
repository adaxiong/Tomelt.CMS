using System.Collections.Generic;

namespace Tomelt.Owin {
    /// <summary>
    /// Represents a provider that makes Owin middlewares available for the Tomelt Owin pipeline.
    /// </summary>
    /// <remarks>
    /// If you want to write an Owin middleware and inject it into the Tomelt Owin pipeline, implement this interface. For more information
    /// about Owin <see cref="!:http://owin.org/">http://owin.org/</see>.
    /// </remarks>
    public interface IOwinMiddlewareProvider : IDependency {
        /// <summary>
        /// Gets <see cref="OwinMiddlewareRegistration"/> objects that will be used to alter the Tomelt Owin pipeline.
        /// </summary>
        /// <returns><see cref="OwinMiddlewareRegistration"/> objects that will be used to alter the Tomelt Owin pipeline.</returns>
        IEnumerable<OwinMiddlewareRegistration> GetOwinMiddlewares();
    }
}